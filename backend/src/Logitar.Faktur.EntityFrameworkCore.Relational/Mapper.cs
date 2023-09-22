using Logitar.EventSourcing;
using Logitar.Faktur.Application;
using Logitar.Faktur.Contracts;
using Logitar.Faktur.Contracts.Actors;
using Logitar.Faktur.Contracts.Banners;
using Logitar.Faktur.Contracts.Departments;
using Logitar.Faktur.Contracts.Stores;
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

  public Department ToDepartment(DepartmentEntity source) => ToDepartment(source, mapStore: true);
  public Department ToDepartment(DepartmentEntity source, bool mapStore)
  {
    Department destination = new()
    {
      Number = source.Number,
      DisplayName = source.DisplayName,
      Description = source.Description,
      Version = source.Version,
      CreatedBy = FindActor(source.CreatedBy),
      CreatedOn = ToUniversalTime(source.CreatedOn),
      UpdatedBy = FindActor(source.UpdatedBy),
      UpdatedOn = ToUniversalTime(source.UpdatedOn),
      Store = (!mapStore || source.Store == null) ? null : ToStore(source.Store, mapDepartments: false)
    };

    return destination;
  }

  public Store ToStore(StoreEntity source) => ToStore(source, mapDepartments: true);
  public Store ToStore(StoreEntity source, bool mapDepartments)
  {
    Store destination = new()
    {
      Number = source.Number,
      DisplayName = source.DisplayName,
      Description = source.Description,
      Banner = source.Banner == null ? null : ToBanner(source.Banner)
    };

    MapAggregate(source, destination);

    if (source.AddressStreet != null && source.AddressLocality != null && source.AddressCountry != null && source.AddressFormatted != null)
    {
      destination.Address = new Address
      {
        Street = source.AddressStreet,
        Locality = source.AddressLocality,
        Region = source.AddressRegion,
        PostalCode = source.AddressPostalCode,
        Country = source.AddressCountry,
        Formatted = source.AddressFormatted
      };
    }
    if (source.PhoneNumber != null && source.PhoneE164Formatted != null)
    {
      destination.Phone = new Phone
      {
        CountryCode = source.PhoneCountryCode,
        Number = source.PhoneNumber,
        Extension = source.PhoneExtension,
        E164Formatted = source.PhoneE164Formatted
      };
    }

    if (mapDepartments)
    {
      destination.Departments = source.Departments.Select(department => ToDepartment(department, mapStore: false));
    }

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
