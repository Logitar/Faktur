using GraphQL.Types;
using Logitar.Faktur.Contracts.Banners;
using Logitar.Faktur.GraphQL.Search;

namespace Logitar.Faktur.GraphQL.Banners;

internal class BannerSortOptionGraphType : SortOptionInputGraphType<BannerSortOption>
{
  public BannerSortOptionGraphType() : base()
  {
    Field(x => x.Field, type: typeof(NonNullGraphType<BannerSortGraphType>))
      .Description("The field on which to apply the sort.");
  }
}
