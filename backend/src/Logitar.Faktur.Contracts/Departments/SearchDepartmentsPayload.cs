using Logitar.Faktur.Contracts.Search;

namespace Logitar.Faktur.Contracts.Departments;

public record SearchDepartmentsPayload : SearchPayload
{
  public new IEnumerable<DepartmentSortOption> Sort { get; set; } = Enumerable.Empty<DepartmentSortOption>();
}
