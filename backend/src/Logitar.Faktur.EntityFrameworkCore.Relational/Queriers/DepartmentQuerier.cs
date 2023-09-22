using Logitar.Data;
using Logitar.EventSourcing;
using Logitar.Faktur.Application.Actors;
using Logitar.Faktur.Application.Departments;
using Logitar.Faktur.Contracts.Actors;
using Logitar.Faktur.Contracts.Departments;
using Logitar.Faktur.Contracts.Search;
using Logitar.Faktur.Domain.Stores;
using Logitar.Faktur.EntityFrameworkCore.Relational.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logitar.Faktur.EntityFrameworkCore.Relational.Queriers;

internal class DepartmentQuerier : IDepartmentQuerier
{
  private readonly IActorService _actorService;
  private readonly DbSet<DepartmentEntity> _departments;
  private readonly ISqlHelper _sqlHelper;

  public DepartmentQuerier(IActorService actorService, FakturContext context, ISqlHelper sqlHelper)
  {
    _actorService = actorService;
    _departments = context.Departments;
    _sqlHelper = sqlHelper;
  }

  public async Task<Department?> ReadAsync(string storeId, string number, CancellationToken cancellationToken)
  {
    storeId = new StoreId(storeId).Value;
    number = number.Trim();

    DepartmentEntity? department = await _departments.AsNoTracking()
      .Include(x => x.Store)
      .SingleOrDefaultAsync(x => x.Store!.AggregateId == storeId && x.Number == number, cancellationToken);

    return department == null ? null : await MapAsync(department, cancellationToken);
  }

  public async Task<SearchResults<Department>> SearchAsync(string storeId, SearchDepartmentsPayload payload, CancellationToken cancellationToken)
  {
    storeId = new StoreId(storeId).Value;

    IQueryBuilder builder = _sqlHelper.QueryFrom(Db.Departments.Table)
      .Join(Db.Stores.StoreId, Db.Departments.StoreId)
      .Where(Db.Stores.AggregateId, Operators.IsEqualTo(storeId))
      .SelectAll(Db.Departments.Table);
    _sqlHelper.ApplyIdSearch(builder, payload.Id, Db.Departments.Number);
    _sqlHelper.ApplyTextSearch(builder, payload.Search, Db.Departments.Number, Db.Departments.DisplayName);

    IQueryable<DepartmentEntity> query = _departments.FromQuery(builder)
      .Include(x => x.Store).ThenInclude(x => x!.Banner)
      .AsNoTracking();

    long total = await query.LongCountAsync(cancellationToken);

    IOrderedQueryable<DepartmentEntity>? ordered = null;
    foreach (DepartmentSortOption sort in payload.Sort)
    {
      switch (sort.Field)
      {
        case DepartmentSort.DisplayName:
          ordered = (ordered == null)
            ? (sort.IsDescending ? query.OrderByDescending(x => x.DisplayName) : query.OrderBy(x => x.DisplayName))
            : (sort.IsDescending ? ordered.OrderByDescending(x => x.DisplayName) : ordered.OrderBy(x => x.DisplayName));
          break;
        case DepartmentSort.Number:
          ordered = (ordered == null)
            ? (sort.IsDescending ? query.OrderByDescending(x => x.Number) : query.OrderBy(x => x.Number))
            : (sort.IsDescending ? ordered.OrderByDescending(x => x.Number) : ordered.OrderBy(x => x.Number));
          break;
        case DepartmentSort.UpdatedOn:
          ordered = (ordered == null)
            ? (sort.IsDescending ? query.OrderByDescending(x => x.UpdatedOn) : query.OrderBy(x => x.UpdatedOn))
            : (sort.IsDescending ? ordered.OrderByDescending(x => x.UpdatedOn) : ordered.OrderBy(x => x.UpdatedOn));
          break;
      }
    }
    query = ordered ?? query;

    query = query.ApplyPaging(payload);

    DepartmentEntity[] departments = await query.ToArrayAsync(cancellationToken);
    IEnumerable<Department> results = await MapAsync(departments, cancellationToken);

    return new SearchResults<Department>(results, total);
  }

  private async Task<Department> MapAsync(DepartmentEntity department, CancellationToken cancellationToken)
   => (await MapAsync(new[] { department }, cancellationToken)).Single();
  private async Task<IEnumerable<Department>> MapAsync(IEnumerable<DepartmentEntity> departments, CancellationToken cancellationToken)
  {
    IEnumerable<ActorId> actorIds = departments.SelectMany(store => store.GetActorIds());
    IEnumerable<Actor> actors = await _actorService.FindAsync(actorIds, cancellationToken);
    Mapper mapper = new(actors);

    return departments.Select(mapper.ToDepartment);
  }
}
