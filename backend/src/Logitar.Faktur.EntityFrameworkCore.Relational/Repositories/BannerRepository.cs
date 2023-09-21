using Logitar.EventSourcing.EntityFrameworkCore.Relational;
using Logitar.EventSourcing.Infrastructure;
using Logitar.Faktur.Domain.Banners;

namespace Logitar.Faktur.EntityFrameworkCore.Relational.Repositories;

internal class BannerRepository : EventSourcing.EntityFrameworkCore.Relational.AggregateRepository, IBannerRepository
{
  public BannerRepository(IEventBus eventBus, EventContext eventContext, IEventSerializer eventSerializer)
    : base(eventBus, eventContext, eventSerializer)
  {
  }

  public async Task<BannerAggregate?> LoadAsync(BannerId id, CancellationToken cancellationToken)
    => await LoadAsync<BannerAggregate>(id.AggregateId, cancellationToken);

  public async Task SaveAsync(BannerAggregate banner, CancellationToken cancellationToken)
    => await base.SaveAsync(banner, cancellationToken);
}
