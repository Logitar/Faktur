using Logitar.EventSourcing.EntityFrameworkCore.Relational;
using Logitar.EventSourcing.Infrastructure;
using Logitar.Faktur.Domain.Stores;

namespace Logitar.Faktur.EntityFrameworkCore.Relational.Repositories;

internal class StoreRepository : EventSourcing.EntityFrameworkCore.Relational.AggregateRepository, IStoreRepository
{
  public StoreRepository(IEventBus eventBus, EventContext eventContext, IEventSerializer eventSerializer)
    : base(eventBus, eventContext, eventSerializer)
  {
  }

  public async Task<StoreAggregate?> LoadAsync(StoreId id, CancellationToken cancellationToken)
    => await LoadAsync<StoreAggregate>(id.AggregateId, cancellationToken);

  public async Task SaveAsync(StoreAggregate store, CancellationToken cancellationToken)
    => await base.SaveAsync(store, cancellationToken);
}
