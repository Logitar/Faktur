using GraphQL.Types;
using Logitar.Faktur.Contracts.Banners;

namespace Logitar.Faktur.GraphQL.Banners;

internal class SearchBannersPayloadGraphType : SearchPayloadInputGraphType<SearchBannersPayload>
{
  public SearchBannersPayloadGraphType() : base()
  {
    Field(x => x.Sort, type: typeof(NonNullGraphType<ListGraphType<NonNullGraphType<BannerSortOptionGraphType>>>))
      .DefaultValue(Enumerable.Empty<BannerSortOption>())
      .Description("The sort parameters of the search.");
  }
}
