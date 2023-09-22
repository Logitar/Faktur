using FluentValidation;

namespace Logitar.Faktur.Domain.Departments;

public record DepartmentNumber
{
  public const int MaximumLength = 10;

  public string Value { get; }

  public DepartmentNumber(string value)
  {
    Value = value.Trim();
    new DepartmentNumberValidator(nameof(DepartmentNumber)).ValidateAndThrow(this);
  }

  public static DepartmentNumber? TryCreate(string? value) => string.IsNullOrWhiteSpace(value) ? null : new(value);
}

public class DepartmentNumberConverter : JsonConverter<DepartmentNumber>
{
  public override DepartmentNumber? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    var value = reader.GetString();

    return value == null ? null : new DepartmentNumber(value);
  }

  public override void Write(Utf8JsonWriter writer, DepartmentNumber displayName, JsonSerializerOptions options)
  {
    writer.WriteStringValue(displayName?.Value);
  }
}

internal class DepartmentNumberValidator : AbstractValidator<DepartmentNumber>
{
  public DepartmentNumberValidator(string? propertyName = null)
  {
    RuleFor(x => x.Value).NotEmpty()
      .MaximumLength(DepartmentNumber.MaximumLength)
      .WithPropertyName(propertyName);
  }
}
