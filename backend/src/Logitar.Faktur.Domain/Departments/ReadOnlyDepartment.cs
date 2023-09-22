namespace Logitar.Faktur.Domain.Departments;

public record ReadOnlyDepartment
{
  public DisplayName DisplayName { get; }
  public Description? Description { get; }

  public ReadOnlyDepartment(DisplayName displayName, Description? description = null)
  {
    DisplayName = displayName;
    Description = description;
  }
}
