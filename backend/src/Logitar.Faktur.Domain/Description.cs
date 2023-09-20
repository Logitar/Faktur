using FluentValidation;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Logitar.Faktur.Domain;

public record Description
{
  public string Value { get; }

  public Description(string value)
  {
    Value = value.Trim();
    new DescriptionValidator(nameof(Description)).ValidateAndThrow(this);
  }

  public static Description? TryCreate(string? value) => string.IsNullOrWhiteSpace(value) ? null : new(value);
}

public class DescriptionConverter : JsonConverter<Description>
{
  public override Description? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    string? value = reader.GetString();

    return value == null ? null : new Description(value);
  }

  public override void Write(Utf8JsonWriter writer, Description displayName, JsonSerializerOptions options)
  {
    writer.WriteStringValue(displayName?.Value);
  }
}

internal class DescriptionValidator : AbstractValidator<Description>
{
  public DescriptionValidator(string? propertyName = null)
  {
    RuleFor(x => x.Value).NotEmpty()
      .WithPropertyName(propertyName);
  }
}
