using Logitar.EventSourcing;
using Logitar.Faktur.Contracts;
using Logitar.Faktur.Domain.Banners;
using MediatR;

namespace Logitar.Faktur.Domain.Stores.Events;

public record StoreUpdatedEvent : DomainEvent, INotification
{
  public Modification<BannerId>? BannerId { get; set; }

  public Modification<StoreNumber>? Number { get; set; }
  public DisplayName? DisplayName { get; set; }
  public Modification<Description>? Description { get; set; }

  public Modification<ReadOnlyAddress>? Address { get; set; }
  public Modification<ReadOnlyPhone>? Phone { get; set; }
}
