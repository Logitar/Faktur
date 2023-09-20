using Logitar.EventSourcing;
using Logitar.Faktur.Contracts;
using Logitar.Faktur.Domain.Banners.Events;

namespace Logitar.Faktur.Domain.Banners;

public class BannerAggregate : AggregateRoot
{
  private DisplayName? _displayName = null;
  private Description? _description = null;

  public new BannerId Id => new(Id.Value);

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

  protected BannerUpdatedEvent UpdatedEvent
  {
    get
    {
      BannerUpdatedEvent? @event = Changes.SingleOrDefault(change => change is BannerUpdatedEvent) as BannerUpdatedEvent;
      if (@event == null)
      {
        @event = new();
        ApplyChange(@event);
      }

      return @event;
    }
  }

  public BannerAggregate(AggregateId id) : base(id)
  {
  }

  public BannerAggregate(DisplayName displayName, ActorId actorId = default, BannerId? id = null)
    : base(id?.AggregateId)
  {
    ApplyChange(new BannerCreatedEvent(actorId, displayName));
  }
  protected virtual void Apply(BannerCreatedEvent @event)
  {
    _displayName = @event.DisplayName;
  }

  public void Delete(ActorId actorId = default) => ApplyChange(new BannerDeletedEvent(actorId));

  public void Update(ActorId actorId = default)
  {
    foreach (DomainEvent change in Changes)
    {
      if (change is BannerUpdatedEvent @event && @event.ActorId == actorId)
      {
        @event.ActorId = actorId;

        if (@event.Version == Version)
        {
          UpdatedBy = actorId;
        }
      }
    }
  }

  protected virtual void Apply(BannerUpdatedEvent @event)
  {
    if (@event.DisplayName != null)
    {
      _displayName = @event.DisplayName;
    }
    if (@event.Description != null)
    {
      _description = @event.Description.Value;
    }
  }

  public override string ToString() => base.ToString();
}
