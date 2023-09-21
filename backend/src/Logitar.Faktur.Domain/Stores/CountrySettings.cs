namespace Logitar.Faktur.Domain.Stores;

internal record CountrySettings
{
  public string? PostalCode { get; init; }
  public HashSet<string>? Regions { get; init; }
}
