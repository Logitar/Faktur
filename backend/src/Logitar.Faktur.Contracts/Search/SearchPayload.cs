namespace Logitar.Faktur.Contracts.Search;

public record SearchPayload
{
  public IEnumerable<Guid> IdIn { get; set; } = Enumerable.Empty<Guid>();
  public TextSearch Search { get; set; } = new();

  public IEnumerable<SortOption> Sort { get; set; } = Enumerable.Empty<SortOption>();

  public int Skip { get; set; }
  public int Limit { get; set; }
}
