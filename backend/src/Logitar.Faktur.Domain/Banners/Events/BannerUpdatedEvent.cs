using Logitar.EventSourcing;
using Logitar.Faktur.Contracts;
using MediatR;

namespace Logitar.Faktur.Domain.Banners.Events;

public record BannerUpdatedEvent : DomainEvent, INotification
{
  public DisplayName? DisplayName { get; set; }
  public Modification<Description>? Description { get; set; }
}
