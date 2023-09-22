using GraphQL.Types;
using Logitar.Faktur.Contracts.Departments;

namespace Logitar.Faktur.GraphQL.Departments;

internal class DepartmentSortGraphType : EnumerationGraphType<DepartmentSort>
{
  public DepartmentSortGraphType()
  {
    Name = nameof(DepartmentSort);
    Description = "Represents the available department fields for sorting.";

    Add(DepartmentSort.DisplayName, "The departments will be sorted by their display name.");
    Add(DepartmentSort.Number, "The departments will be sorted by their number.");
    Add(DepartmentSort.UpdatedOn, "The departments will be sorted by their latest update date and time.");
  }

  private void Add(DepartmentSort value, string description) => Add(value.ToString(), value, description);
}
