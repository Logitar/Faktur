namespace Logitar.Faktur.Contracts.Stores;

public record PhonePayload
{
  public string? CountryCode { get; set; }
  public string Number { get; set; } = string.Empty;
}
