using Logitar.Faktur.Contracts.Banners;

namespace Logitar.Faktur.GraphQL.Banners;

internal class BannerSearchResultsGraphType : SearchResultsGraphType<BannerGraphType, Banner>
{
  public BannerSearchResultsGraphType() : base("BannerSearchResults", "Represents the results of a banner search.")
  {
  }
}
