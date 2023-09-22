namespace Logitar.Faktur.Contracts.Departments;

public record UpdateDepartmentPayload
{
  public string? DisplayName { get; set; } = string.Empty;
  public Modification<string>? Description { get; set; }
}
