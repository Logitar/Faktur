using Logitar.Faktur.Contracts.Departments;

namespace Logitar.Faktur.GraphQL.Departments;

internal class DepartmentSearchResultsGraphType : SearchResultsGraphType<DepartmentGraphType, Department>
{
  public DepartmentSearchResultsGraphType() : base("DepartmentSearchResults", "Represents the results of a department search.")
  {
  }
}
