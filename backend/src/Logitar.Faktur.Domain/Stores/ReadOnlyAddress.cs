using FluentValidation;
using Logitar.Faktur.Contracts.Stores;

namespace Logitar.Faktur.Domain.Stores;

public record ReadOnlyAddress : IPostalAddress
{
  public const int StreetMaximumLength = byte.MaxValue;
  public const int LocalityMaximumLength = 100;
  public const int RegionMaximumLength = 2;
  public const int PostalCodeMaximumLength = 10;
  public const int CountryMaximumLength = 2;

  public string Street { get; }
  public string Locality { get; }
  public string? Region { get; }
  public string? PostalCode { get; }
  public string Country { get; }
  //public string Formatted => this.Format(); // TODO(fpion): PostalAddressHelper

  public ReadOnlyAddress(string street, string locality, string country, string? region = null, string? postalCode = null)
  {
    Street = street.Trim();
    Locality = locality.Trim();
    Region = region?.CleanTrim();
    PostalCode = postalCode?.CleanTrim();
    Country = country.Trim();

    new ReadOnlyAddressValidator(nameof(Address)).ValidateAndThrow(this);
  }

  public static ReadOnlyAddress? TryCreate(AddressPayload? address) => address == null ? null
    : new(address.Street, address.Locality, address.Country, address.Region, address.PostalCode);
}

internal class ReadOnlyAddressValidator : AbstractValidator<ReadOnlyAddress>
{
  public ReadOnlyAddressValidator(string? propertyName = null)
  {
    RuleFor(x => x.Street).NotEmpty()
      .MaximumLength(ReadOnlyAddress.StreetMaximumLength)
      .WithPropertyName(BuildPropertyName(propertyName, nameof(IPostalAddress.Street)));

    RuleFor(x => x.Locality).NotEmpty()
      .MaximumLength(ReadOnlyAddress.LocalityMaximumLength)
      .WithPropertyName(BuildPropertyName(propertyName, nameof(IPostalAddress.Locality)));

    // TODO(fpion): validate Region

    // TODO(fpion): validate Region

    RuleFor(x => x.Country).NotEmpty()
      .MaximumLength(ReadOnlyAddress.CountryMaximumLength)
      .WithPropertyName(BuildPropertyName(propertyName, nameof(IPostalAddress.Country))); // TODO(fpion): validate Country
  }

  private static string? BuildPropertyName(string? baseName, string propertyName)
    => baseName == null ? null : string.Join('.', baseName, propertyName);
}
