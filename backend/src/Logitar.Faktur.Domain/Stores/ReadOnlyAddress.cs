using FluentValidation;
using Logitar.Faktur.Contracts.Stores;
using System.Text;

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

  public string Format()
  {
    StringBuilder formatted = new();

    string[] lines = Street.Remove("\r").Split('\n');
    foreach (string line in lines)
    {
      formatted.AppendLine(line);
    }

    formatted.Append(Locality);
    if (Region != null)
    {
      formatted.Append(' ').Append(Region);
    }
    if (PostalCode != null)
    {
      formatted.Append(' ').Append(PostalCode);
    }
    formatted.AppendLine();

    formatted.Append(Country);

    return formatted.ToString();
  }
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

    When(x => PostalAddressHelper.GetCountry(x.Country)?.Regions != null,
      () => RuleFor(x => x.Region).NotEmpty()
        .MaximumLength(ReadOnlyAddress.RegionMaximumLength)
        .Must((address, country) => PostalAddressHelper.GetCountry(address.Country)!.Regions!.Contains(country))
          .WithErrorCode("RegionValidator")
          .WithMessage(x => $"'{{PropertyName}}' must be one of the following: {string.Join(", ", PostalAddressHelper.GetCountry(x.Country)!.Regions!)}.")
        .WithPropertyName(BuildPropertyName(propertyName, nameof(IPostalAddress.Region)))
    ).Otherwise(() => When(x => x.Region != null,
      () => RuleFor(x => x.Region).NotEmpty()
        .MaximumLength(ReadOnlyAddress.RegionMaximumLength)
        .WithPropertyName(BuildPropertyName(propertyName, nameof(IPostalAddress.Region)))
    ));

    When(x => PostalAddressHelper.GetCountry(x.Country)?.PostalCode != null,
      () => RuleFor(x => x.PostalCode).NotEmpty()
        .MaximumLength(ReadOnlyAddress.PostalCodeMaximumLength)
        .Matches(x => PostalAddressHelper.GetCountry(x.Country)!.PostalCode)
        .WithPropertyName(BuildPropertyName(propertyName, nameof(IPostalAddress.PostalCode)))
    ).Otherwise(() => When(x => x.PostalCode != null,
      () => RuleFor(x => x.PostalCode).NotEmpty()
        .MaximumLength(ReadOnlyAddress.PostalCodeMaximumLength)
        .WithPropertyName(BuildPropertyName(propertyName, nameof(IPostalAddress.PostalCode)))
    ));

    RuleFor(x => x.Country).NotEmpty()
      .MaximumLength(ReadOnlyAddress.CountryMaximumLength)
      .Must(PostalAddressHelper.IsSupported)
        .WithErrorCode("CountryValidator")
        .WithMessage($"'{{PropertyName}}' must be one of the following: {string.Join(", ", PostalAddressHelper.SupportedCountries)}.")
      .WithPropertyName(BuildPropertyName(propertyName, nameof(IPostalAddress.Country)));
  }

  private static string? BuildPropertyName(string? baseName, string propertyName)
    => baseName == null ? null : string.Join('.', baseName, propertyName);
}
