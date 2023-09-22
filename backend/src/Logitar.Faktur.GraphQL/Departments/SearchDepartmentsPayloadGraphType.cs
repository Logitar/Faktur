using GraphQL.Types;
using Logitar.Faktur.Contracts.Departments;

namespace Logitar.Faktur.GraphQL.Departments;

internal class SearchDepartmentsPayloadGraphType : SearchPayloadInputGraphType<SearchDepartmentsPayload>
{
  public SearchDepartmentsPayloadGraphType() : base()
  {
    Field(x => x.Sort, type: typeof(NonNullGraphType<ListGraphType<NonNullGraphType<DepartmentSortOptionGraphType>>>))
      .DefaultValue(Enumerable.Empty<DepartmentSortOption>())
      .Description("The sort parameters of the search.");
  }
}
