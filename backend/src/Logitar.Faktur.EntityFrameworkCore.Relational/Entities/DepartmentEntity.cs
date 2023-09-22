using Logitar.EventSourcing;

namespace Logitar.Faktur.EntityFrameworkCore.Relational.Entities;

internal class DepartmentEntity
{
  public int DepartmentId { get; private set; }

  public StoreEntity? Store { get; private set; }
  public int StoreId { get; private set; }

  public string Number { get; private set; } = string.Empty;
  public string DisplayName { get; private set; } = string.Empty;
  public string? Description { get; private set; }

  public long Version { get; private set; }

  public string CreatedBy { get; private set; } = string.Empty;
  public DateTime CreatedOn { get; private set; }

  public string UpdatedBy { get; private set; } = string.Empty;
  public DateTime UpdatedOn { get; private set; }

  private DepartmentEntity()
  {
  }

  public IEnumerable<ActorId> GetActorIds() => new ActorId[] { new(CreatedBy), new(UpdatedBy) };
}
