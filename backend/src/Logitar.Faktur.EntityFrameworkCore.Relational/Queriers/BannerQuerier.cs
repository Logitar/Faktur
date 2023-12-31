﻿using Logitar.Data;
using Logitar.EventSourcing;
using Logitar.Faktur.Application.Actors;
using Logitar.Faktur.Application.Banners;
using Logitar.Faktur.Contracts.Actors;
using Logitar.Faktur.Contracts.Banners;
using Logitar.Faktur.Contracts.Search;
using Logitar.Faktur.Domain.Banners;
using Logitar.Faktur.EntityFrameworkCore.Relational.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logitar.Faktur.EntityFrameworkCore.Relational.Queriers;

internal class BannerQuerier : IBannerQuerier
{
  private readonly IActorService _actorService;
  private readonly DbSet<BannerEntity> _banners;
  private readonly ISqlHelper _sqlHelper;

  public BannerQuerier(IActorService actorService, FakturContext context, ISqlHelper sqlHelper)
  {
    _actorService = actorService;
    _banners = context.Banners;
    _sqlHelper = sqlHelper;
  }

  public async Task<Banner?> ReadAsync(string id, CancellationToken cancellationToken)
  {
    string aggregateId = new BannerId(id).Value;

    BannerEntity? banner = await _banners.AsNoTracking()
      .SingleOrDefaultAsync(x => x.AggregateId == aggregateId, cancellationToken);

    return banner == null ? null : await MapAsync(banner, cancellationToken);
  }

  public async Task<SearchResults<Banner>> SearchAsync(SearchBannersPayload payload, CancellationToken cancellationToken)
  {
    IQueryBuilder builder = _sqlHelper.QueryFrom(Db.Banners.Table)
      .SelectAll(Db.Banners.Table);
    _sqlHelper.ApplyIdSearch(builder, payload.Id, Db.Banners.AggregateId);
    _sqlHelper.ApplyTextSearch(builder, payload.Search, Db.Banners.DisplayName);

    IQueryable<BannerEntity> query = _banners.FromQuery(builder)
      .AsNoTracking();

    long total = await query.LongCountAsync(cancellationToken);

    IOrderedQueryable<BannerEntity>? ordered = null;
    foreach (BannerSortOption sort in payload.Sort)
    {
      switch (sort.Field)
      {
        case BannerSort.DisplayName:
          ordered = (ordered == null)
            ? (sort.IsDescending ? query.OrderByDescending(x => x.DisplayName) : query.OrderBy(x => x.DisplayName))
            : (sort.IsDescending ? ordered.OrderByDescending(x => x.DisplayName) : ordered.OrderBy(x => x.DisplayName));
          break;
        case BannerSort.UpdatedOn:
          ordered = (ordered == null)
            ? (sort.IsDescending ? query.OrderByDescending(x => x.UpdatedOn) : query.OrderBy(x => x.UpdatedOn))
            : (sort.IsDescending ? ordered.OrderByDescending(x => x.UpdatedOn) : ordered.OrderBy(x => x.UpdatedOn));
          break;
      }
    }
    query = ordered ?? query;

    query = query.ApplyPaging(payload);

    BannerEntity[] banners = await query.ToArrayAsync(cancellationToken);
    IEnumerable<Banner> results = await MapAsync(banners, cancellationToken);

    return new SearchResults<Banner>(results, total);
  }

  private async Task<Banner> MapAsync(BannerEntity banner, CancellationToken cancellationToken)
    => (await MapAsync(new[] { banner }, cancellationToken)).Single();
  private async Task<IEnumerable<Banner>> MapAsync(IEnumerable<BannerEntity> banners, CancellationToken cancellationToken)
  {
    IEnumerable<ActorId> actorIds = banners.SelectMany(banner => banner.GetActorIds());
    IEnumerable<Actor> actors = await _actorService.FindAsync(actorIds, cancellationToken);
    Mapper mapper = new(actors);

    return banners.Select(mapper.ToBanner);
  }
}
