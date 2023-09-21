using Logitar.EventSourcing;
using MediatR;

namespace Logitar.Faktur.Domain.Banners.Events;

public record BannerCreatedEvent : DomainEvent, INotification
{
  public BannerCreatedEvent(ActorId actorId, DisplayName displayName)
  {
    ActorId = actorId;
    DisplayName = displayName;
  }

  public DisplayName DisplayName { get; init; }
}
