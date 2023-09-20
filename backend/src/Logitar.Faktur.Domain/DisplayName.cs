using FluentValidation;

namespace Logitar.Faktur.Domain;

public record DisplayName
{
  public const int MaximumLength = 50;

  public string Value { get; }

  public DisplayName(string value)
  {
    Value = value.Trim();
    new DisplayNameValidator(nameof(DisplayName)).ValidateAndThrow(this);
  }
}

public class DisplayNameConverter : JsonConverter<DisplayName>
{
  public override DisplayName? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    string? value = reader.GetString();

    return value == null ? null : new DisplayName(value);
  }

  public override void Write(Utf8JsonWriter writer, DisplayName displayName, JsonSerializerOptions options)
  {
    writer.WriteStringValue(displayName?.Value);
  }
}

internal class DisplayNameValidator : AbstractValidator<DisplayName>
{
  public DisplayNameValidator(string? propertyName = null)
  {
    RuleFor(x => x.Value).NotEmpty()
      .MaximumLength(DisplayName.MaximumLength)
      .WithPropertyName(propertyName);
  }
}
