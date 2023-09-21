using Logitar.EventSourcing;
using Logitar.Faktur.Contracts;
using Logitar.Faktur.Domain.Banners;
using Logitar.Faktur.Domain.Stores.Events;

namespace Logitar.Faktur.Domain.Stores;

public class StoreAggregate : AggregateRoot
{
  private BannerId? _bannerId = null;

  private StoreNumber? _number = null;
  private DisplayName? _displayName = null;
  private Description? _description = null;

  private ReadOnlyAddress? _address = null;
  private ReadOnlyPhone? _phone = null;

  public new StoreId Id => new(base.Id.Value);

  public BannerId? BannerId
  {
    get => _bannerId;
    set
    {
      if (value != _bannerId)
      {
        UpdatedEvent.BannerId = new Modification<BannerId>(value);
        _bannerId = value;
      }
    }
  }

  public StoreNumber? Number
  {
    get => _number;
    set
    {
      if (value != _number)
      {
        UpdatedEvent.Number = new Modification<StoreNumber>(value);
        _number = value;
      }
    }
  }
  public DisplayName DisplayName
  {
    get => _displayName ?? throw new InvalidOperationException($"The {nameof(DisplayName)} has not been initialized yet.");
    set
    {
      if (value != _displayName)
      {
        UpdatedEvent.DisplayName = value;
        _displayName = value;
      }
    }
  }
  public Description? Description
  {
    get => _description;
    set
    {
      if (value != _description)
      {
        UpdatedEvent.Description = new Modification<Description>(value);
        _description = value;
      }
    }
  }

  public ReadOnlyAddress? Address
  {
    get => _address;
    set
    {
      if (value != _address)
      {
        UpdatedEvent.Address = new Modification<ReadOnlyAddress>(value);
        _address = value;
      }
    }
  }
  public ReadOnlyPhone? Phone
  {
    get => _phone;
    set
    {
      if (value != _phone)
      {
        UpdatedEvent.Phone = new Modification<ReadOnlyPhone>(value);
        _phone = value;
      }
    }
  }

  protected StoreUpdatedEvent UpdatedEvent
  {
    get
    {
      StoreUpdatedEvent? @event = Changes.SingleOrDefault(change => change is StoreUpdatedEvent) as StoreUpdatedEvent;
      if (@event == null)
      {
        @event = new();
        ApplyChange(@event);
      }

      return @event;
    }
  }

  public StoreAggregate(AggregateId id) : base(id)
  {
  }

  public StoreAggregate(DisplayName displayName, ActorId actorId = default, StoreId? id = null)
    : base(id?.AggregateId)
  {
    ApplyChange(new StoreCreatedEvent(actorId, displayName));
  }
  protected virtual void Apply(StoreCreatedEvent @event)
  {
    _displayName = @event.DisplayName;
  }

  public void Delete(ActorId actorId = default) => ApplyChange(new StoreDeletedEvent(actorId));

  public void Update(ActorId actorId = default)
  {
    foreach (DomainEvent change in Changes)
    {
      if (change is StoreUpdatedEvent @event && @event.ActorId == actorId)
      {
        @event.ActorId = actorId;

        if (@event.Version == Version)
        {
          UpdatedBy = actorId;
        }
      }
    }
  }

  protected virtual void Apply(StoreUpdatedEvent @event)
  {
    if (@event.BannerId != null)
    {
      _bannerId = @event.BannerId.Value;
    }

    if (@event.Number != null)
    {
      _number = @event.Number.Value;
    }
    if (@event.DisplayName != null)
    {
      _displayName = @event.DisplayName;
    }
    if (@event.Description != null)
    {
      _description = @event.Description.Value;
    }

    if (@event.Phone != null)
    {
      _phone = @event.Phone.Value;
    }
  }

  public override string ToString() => $"{DisplayName.Value} | {base.ToString()}";
}
