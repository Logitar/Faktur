using Logitar.Faktur.Contracts.Stores;
using PhoneNumbers;

namespace Logitar.Faktur.Domain.Stores;

internal static class PhoneExtensions
{
  private const string DefaultRegion = "CA";

  public static bool IsValid(this IPhone phone)
  {
    try
    {
      _ = phone.Parse();

      return true;
    }
    catch (NumberParseException)
    {
      return false;
    }
  }

  public static string FormatToE164(this IPhone phone)
  {
    PhoneNumber phoneNumber = phone.Parse();

    return PhoneNumberUtil.GetInstance().Format(phoneNumber, PhoneNumberFormat.E164);
  }

  private static PhoneNumber Parse(this IPhone phone)
  {
    string formattedPhone = string.IsNullOrWhiteSpace(phone.Extension)
      ? phone.Number
      : $"{phone.Number} x{phone.Extension}";

    return PhoneNumberUtil.GetInstance().Parse(formattedPhone.ToString(), phone.CountryCode ?? DefaultRegion);
  }
}
