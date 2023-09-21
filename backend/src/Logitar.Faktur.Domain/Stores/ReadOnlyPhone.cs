﻿using FluentValidation;
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
  public string E164Formatted => ""; // this.Format(); // TODO(fpion): PhoneHelper

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
      //.Must(p => p.IsValid()) // TODO(fpion): PhoneHelper
      .WithPropertyName(BuildPropertyName(propertyName, nameof(IPhone.Number)));

    When(x => x.Extension != null, () => RuleFor(x => x.Extension).NotEmpty()
      .MaximumLength(ReadOnlyPhone.ExtensionMaximumLength)
      .WithPropertyName(BuildPropertyName(propertyName, nameof(IPhone.Extension))));
  }

  private static string? BuildPropertyName(string? baseName, string propertyName)
    => baseName == null ? null : string.Join('.', baseName, propertyName);
}
