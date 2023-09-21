using GraphQL.Types;
using Logitar.Faktur.Contracts.Stores;

namespace Logitar.Faktur.GraphQL.Stores;

internal class SearchStoresPayloadGraphType : SearchPayloadInputGraphType<SearchStoresPayload>
{
  public SearchStoresPayloadGraphType() : base()
  {
    Field(x => x.BannerId, nullable: true)
      .Description("The unique identifier of the banner in which to search stores.");

    Field(x => x.Sort, type: typeof(NonNullGraphType<ListGraphType<NonNullGraphType<StoreSortOptionGraphType>>>))
      .DefaultValue(Enumerable.Empty<StoreSortOption>())
      .Description("The sort parameters of the search.");
  }
}
