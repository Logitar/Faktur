using Logitar.Faktur.Domain.Stores.Events;

namespace Logitar.Faktur.Infrastructure.Handlers;

public interface IStoreEventHandler
{
  Task HandleAsync(StoreCreatedEvent @event, CancellationToken cancellationToken = default);
  Task HandleAsync(StoreDeletedEvent @event, CancellationToken cancellationToken = default);
  Task<bool> HandleAsync(StoreUpdatedEvent @event, CancellationToken cancellationToken = default);
}
