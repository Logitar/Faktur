using GraphQL.Types;
using Logitar.Faktur.Contracts.Departments;
using Logitar.Faktur.GraphQL.Search;

namespace Logitar.Faktur.GraphQL.Departments;

internal class DepartmentSortOptionGraphType : SortOptionInputGraphType<DepartmentSortOption>
{
  public DepartmentSortOptionGraphType() : base()
  {
    Field(x => x.Field, type: typeof(NonNullGraphType<DepartmentSortGraphType>))
      .Description("The field on which to apply the sort.");
  }
}
