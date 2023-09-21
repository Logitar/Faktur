using GraphQL.Types;
using Logitar.Faktur.Contracts.Stores;
using Logitar.Faktur.GraphQL.Search;

namespace Logitar.Faktur.GraphQL.Stores;

internal class StoreSortOptionGraphType : SortOptionInputGraphType<StoreSortOption>
{
  public StoreSortOptionGraphType() : base()
  {
    Field(x => x.Field, type: typeof(NonNullGraphType<StoreSortGraphType>))
      .Description("The field on which to apply the sort.");
  }
}
