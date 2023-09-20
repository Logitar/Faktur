using Logitar.Faktur.Contracts.Actors;

namespace Logitar.Faktur.EntityFrameworkCore.Relational.Entities;

internal class ActorEntity
{
  public int ActorId { get; private set; }

  public string Id { get; private set; } = string.Empty;
  public ActorType Type { get; private set; }
  public bool IsDeleted { get; private set; }

  public string DisplayName { get; private set; } = string.Empty;
  public string? EmailAddress { get; private set; }
  public string? PictureUrl { get; private set; }

  private ActorEntity()
  {
  }
}
