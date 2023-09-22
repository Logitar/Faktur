using Logitar.Faktur.Contracts.Actors;
using Logitar.Faktur.Contracts.Stores;

namespace Logitar.Faktur.Contracts.Departments;

public class Department
{
  public string Number { get; set; } = string.Empty;
  public string DisplayName { get; set; } = string.Empty;
  public string? Description { get; set; }

  public long Version { get; set; }

  public Actor CreatedBy { get; set; } = new();
  public DateTime CreatedOn { get; set; }

  public Actor UpdatedBy { get; set; } = new();
  public DateTime UpdatedOn { get; set; }

  public Store? Store { get; set; }
}
