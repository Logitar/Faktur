using Logitar.EventSourcing;
using Logitar.Faktur.Application;
using Logitar.Faktur.Contracts;
using Logitar.Faktur.Contracts.Actors;
using Logitar.Faktur.Contracts.Banners;
using Logitar.Faktur.EntityFrameworkCore.Relational.Entities;

namespace Logitar.Faktur.EntityFrameworkCore.Relational;

internal class Mapper
{
  private readonly Dictionary<ActorId, Actor> _actors;

  public Mapper()
  {
    _actors = new();
  }

  public Mapper(IEnumerable<Actor> actors) : this()
  {
    foreach (Actor actor in actors)
    {
      ActorId id = new(actor.Id);
      _actors[id] = actor;
    }
  }

  public Banner ToBanner(BannerEntity source)
  {
    Banner destination = new()
    {
      DisplayName = source.DisplayName,
      Description = source.Description
    };

    MapAggregate(source, destination);

    return destination;
  }

  private void MapAggregate(AggregateEntity source, Aggregate destination)
  {
    destination.Id = new AggregateId(source.AggregateId).Format();
    destination.Version = source.Version;

    destination.CreatedBy = FindActor(source.CreatedBy);
    destination.CreatedOn = ToUniversalTime(source.CreatedOn);

    destination.UpdatedBy = FindActor(source.UpdatedBy);
    destination.UpdatedOn = ToUniversalTime(source.UpdatedOn);
  }

  private Actor FindActor(string id) => FindActor(new ActorId(id));
  private Actor FindActor(ActorId id) => _actors.TryGetValue(id, out Actor? actor) ? actor : new();

  private static DateTime ToUniversalTime(DateTime value) => DateTime.SpecifyKind(value, DateTimeKind.Utc);
}
