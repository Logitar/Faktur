using FluentValidation;
using Logitar.Faktur.Contracts.Stores;

namespace Logitar.Faktur.Domain.Stores;

public record ReadOnlyPhone : IPhone
{
  public const int CountryCodeMaximumLength = 2;
  public const int NumberMaximumLength = 20;
  public const int ExtensionMaximumLength = 10;

  public string? CountryCode { get; }
  public string Number { get; }
  public string? Extension { get; }
  public string E164Formatted => this.FormatToE164();

  public ReadOnlyPhone(string number, string? countryCode = null, string? extension = null)
  {
    CountryCode = countryCode?.CleanTrim();
    Number = number;
    Extension = extension?.CleanTrim();

    new ReadOnlyPhoneValidator(nameof(Phone)).ValidateAndThrow(this);
  }

  public static ReadOnlyPhone? TryCreate(PhonePayload? phone) => phone == null ? null
    : new(phone.Number, phone.CountryCode, phone.Extension);
}

internal class ReadOnlyPhoneValidator : AbstractValidator<ReadOnlyPhone>
{
  public ReadOnlyPhoneValidator(string? propertyName = null)
  {
    When(x => x.CountryCode != null, () => RuleFor(x => x.CountryCode).NotEmpty()
      .MaximumLength(ReadOnlyPhone.CountryCodeMaximumLength)
      .WithPropertyName(BuildPropertyName(propertyName, nameof(IPhone.CountryCode))));

    RuleFor(x => x.Number).NotEmpty()
      .MaximumLength(ReadOnlyPhone.NumberMaximumLength)
      .WithPropertyName(BuildPropertyName(propertyName, nameof(IPhone.Number)));

    When(x => x.Extension != null, () => RuleFor(x => x.Extension).NotEmpty()
      .MaximumLength(ReadOnlyPhone.ExtensionMaximumLength)
      .WithPropertyName(BuildPropertyName(propertyName, nameof(IPhone.Extension))));

    RuleFor(x => x)
      .Must(x => x.IsValid())
        .WithErrorCode("PhoneValidator")
        .WithMessage("'{PropertyName}' must be a valid phone number.")
      .WithPropertyName(propertyName);
  }

  private static string? BuildPropertyName(string? baseName, string propertyName)
    => baseName == null ? null : string.Join('.', baseName, propertyName);
}
