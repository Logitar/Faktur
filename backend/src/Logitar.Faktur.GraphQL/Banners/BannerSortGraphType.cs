using GraphQL.Types;
using Logitar.Faktur.Contracts.Banners;

namespace Logitar.Faktur.GraphQL.Banners;

internal class BannerSortGraphType : EnumerationGraphType<BannerSort>
{
  public BannerSortGraphType()
  {
    Name = nameof(BannerSort);
    Description = "Represents the available banner fields for sorting.";

    Add(BannerSort.DisplayName, "The banners will be sorted by their display name.");
    Add(BannerSort.UpdatedOn, "The banners will be sorted by their latest update date and time.");
  }

  private void Add(BannerSort value, string description) => Add(value.ToString(), value, description);
}
