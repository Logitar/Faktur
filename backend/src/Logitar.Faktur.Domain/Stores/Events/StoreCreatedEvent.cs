using Logitar.EventSourcing;
using MediatR;

namespace Logitar.Faktur.Domain.Stores.Events;

public record StoreCreatedEvent : DomainEvent, INotification
{
  public StoreCreatedEvent(ActorId actorId, DisplayName displayName)
  {
    ActorId = actorId;
    DisplayName = displayName;
  }

  public DisplayName DisplayName { get; init; }
}
