using FluentValidation;

namespace Logitar.Faktur.Domain.Stores;

public record StoreNumber
{
  public const int MaximumLength = 10;

  public string Value { get; }

  public StoreNumber(string value)
  {
    Value = value.Trim();
    new StoreNumberValidator(nameof(StoreNumber)).ValidateAndThrow(this);
  }
}

public class StoreNumberConverter : JsonConverter<StoreNumber>
{
  public override StoreNumber? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    var value = reader.GetString();

    return value == null ? null : new StoreNumber(value);
  }

  public override void Write(Utf8JsonWriter writer, StoreNumber displayName, JsonSerializerOptions options)
  {
    writer.WriteStringValue(displayName?.Value);
  }
}

internal class StoreNumberValidator : AbstractValidator<StoreNumber>
{
  public StoreNumberValidator(string? propertyName = null)
  {
    RuleFor(x => x.Value).NotEmpty()
      .MaximumLength(StoreNumber.MaximumLength)
      .WithPropertyName(propertyName);
  }
}
