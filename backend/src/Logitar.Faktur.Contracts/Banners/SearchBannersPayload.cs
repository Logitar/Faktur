using Logitar.Faktur.Contracts.Search;

namespace Logitar.Faktur.Contracts.Banners;

public record SearchBannersPayload : SearchPayload
{
  public new IEnumerable<BannerSortOption> Sort { get; set; } = Enumerable.Empty<BannerSortOption>();
}
