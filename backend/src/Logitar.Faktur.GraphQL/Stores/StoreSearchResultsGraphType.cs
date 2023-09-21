using Logitar.Faktur.Contracts.Stores;

namespace Logitar.Faktur.GraphQL.Stores;

internal class StoreSearchResultsGraphType : SearchResultsGraphType<StoreGraphType, Store>
{
  public StoreSearchResultsGraphType() : base("StoreSearchResults", "Represents the results of a store search.")
  {
  }
}
