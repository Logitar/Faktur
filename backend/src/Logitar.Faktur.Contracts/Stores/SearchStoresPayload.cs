using Logitar.Faktur.Contracts.Search;

namespace Logitar.Faktur.Contracts.Stores;

public record SearchStoresPayload : SearchPayload
{
  public string? BannerId { get; set; }

  public new IEnumerable<StoreSortOption> Sort { get; set; } = Enumerable.Empty<StoreSortOption>();
}
