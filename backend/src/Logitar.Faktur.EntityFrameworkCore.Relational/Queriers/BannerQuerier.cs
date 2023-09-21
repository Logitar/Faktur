using Logitar.EventSourcing;
using Logitar.Faktur.Application.Actors;
using Logitar.Faktur.Application.Banners;
using Logitar.Faktur.Contracts.Actors;
using Logitar.Faktur.Contracts.Banners;
using Logitar.Faktur.Domain.Banners;
using Logitar.Faktur.EntityFrameworkCore.Relational.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logitar.Faktur.EntityFrameworkCore.Relational.Queriers;

internal class BannerQuerier : IBannerQuerier
{
  private readonly IActorService _actorService;
  private readonly DbSet<BannerEntity> _banners;

  public BannerQuerier(IActorService actorService, FakturContext context)
  {
    _actorService = actorService;
    _banners = context.Banners;
  }

  public async Task<Banner?> ReadAsync(string id, CancellationToken cancellationToken)
  {
    string aggregateId = new BannerId(id).Value;

    BannerEntity? banner = await _banners.AsNoTracking()
      .SingleOrDefaultAsync(x => x.AggregateId == aggregateId, cancellationToken);
    if (banner == null)
    {
      return null;
    }

    IEnumerable<ActorId> actorIds = banner.ActorIds;
    IEnumerable<Actor> actors = await _actorService.FindAsync(actorIds, cancellationToken);
    Mapper mapper = new(actors);

    return mapper.ToBanner(banner);
  }
}
