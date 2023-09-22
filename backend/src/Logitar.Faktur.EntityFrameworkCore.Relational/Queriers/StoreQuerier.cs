using Logitar.Data;
using Logitar.EventSourcing;
using Logitar.Faktur.Application.Actors;
using Logitar.Faktur.Application.Stores;
using Logitar.Faktur.Contracts.Actors;
using Logitar.Faktur.Contracts.Search;
using Logitar.Faktur.Contracts.Stores;
using Logitar.Faktur.Domain.Banners;
using Logitar.Faktur.Domain.Stores;
using Logitar.Faktur.EntityFrameworkCore.Relational.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logitar.Faktur.EntityFrameworkCore.Relational.Queriers;

internal class StoreQuerier : IStoreQuerier
{
  private readonly IActorService _actorService;
  private readonly ISqlHelper _sqlHelper;
  private readonly DbSet<StoreEntity> _stores;

  public StoreQuerier(IActorService actorService, FakturContext context, ISqlHelper sqlHelper)
  {
    _actorService = actorService;
    _sqlHelper = sqlHelper;
    _stores = context.Stores;
  }

  public async Task<Store?> ReadAsync(string id, CancellationToken cancellationToken)
  {
    string aggregateId = new StoreId(id).Value;

    StoreEntity? store = await _stores.AsNoTracking()
      .Include(x => x.Banner)
      .Include(x => x.Departments)
      .SingleOrDefaultAsync(x => x.AggregateId == aggregateId, cancellationToken);

    return store == null ? null : await MapAsync(store, cancellationToken);
  }

  public async Task<SearchResults<Store>> SearchAsync(SearchStoresPayload payload, CancellationToken cancellationToken)
  {
    string? bannerId = string.IsNullOrWhiteSpace(payload.BannerId) ? null : new BannerId(payload.BannerId).Value;

    IQueryBuilder builder = _sqlHelper.QueryFrom(Db.Stores.Table)
      .LeftJoin(Db.Banners.BannerId, Db.Stores.BannerId)
      .Where(Db.Banners.AggregateId, bannerId == null ? Operators.IsNull() : Operators.IsEqualTo(bannerId))
      .SelectAll(Db.Stores.Table);
    _sqlHelper.ApplyIdSearch(builder, payload.Id, Db.Stores.AggregateId);
    _sqlHelper.ApplyTextSearch(builder, payload.Search, Db.Stores.Number, Db.Stores.DisplayName,
      Db.Stores.AddressFormatted, Db.Stores.PhoneE164Formatted);

    IQueryable<StoreEntity> query = _stores.FromQuery(builder)
      .Include(x => x.Banner)
      .Include(x => x.Departments)
      .AsNoTracking();

    long total = await query.LongCountAsync(cancellationToken);

    IOrderedQueryable<StoreEntity>? ordered = null;
    foreach (StoreSortOption sort in payload.Sort)
    {
      switch (sort.Field)
      {
        case StoreSort.DisplayName:
          ordered = (ordered == null)
            ? (sort.IsDescending ? query.OrderByDescending(x => x.DisplayName) : query.OrderBy(x => x.DisplayName))
            : (sort.IsDescending ? ordered.OrderByDescending(x => x.DisplayName) : ordered.OrderBy(x => x.DisplayName));
          break;
        case StoreSort.UpdatedOn:
          ordered = (ordered == null)
            ? (sort.IsDescending ? query.OrderByDescending(x => x.UpdatedOn) : query.OrderBy(x => x.UpdatedOn))
            : (sort.IsDescending ? ordered.OrderByDescending(x => x.UpdatedOn) : ordered.OrderBy(x => x.UpdatedOn));
          break;
      }
    }
    query = ordered ?? query;

    query = query.ApplyPaging(payload);

    StoreEntity[] stores = await query.ToArrayAsync(cancellationToken);
    IEnumerable<Store> results = await MapAsync(stores, cancellationToken);

    return new SearchResults<Store>(results, total);
  }

  private async Task<Store> MapAsync(StoreEntity store, CancellationToken cancellationToken)
    => (await MapAsync(new[] { store }, cancellationToken)).Single();
  private async Task<IEnumerable<Store>> MapAsync(IEnumerable<StoreEntity> stores, CancellationToken cancellationToken)
  {
    IEnumerable<ActorId> actorIds = stores.SelectMany(store => store.GetActorIds());
    IEnumerable<Actor> actors = await _actorService.FindAsync(actorIds, cancellationToken);
    Mapper mapper = new(actors);

    return stores.Select(mapper.ToStore);
  }
}
