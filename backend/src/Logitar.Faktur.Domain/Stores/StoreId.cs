using Logitar.EventSourcing;

namespace Logitar.Faktur.Domain.Stores;

public record StoreId
{
  public AggregateId AggregateId { get; }
  public string Value => AggregateId.Value;

  public StoreId(string value)
    : this(Guid.TryParse(value.Trim(), out Guid guid) ? new AggregateId(guid) : new(value))
  {
  }
  public StoreId(AggregateId aggregateId)
  {
    AggregateId = aggregateId;
  }
}

public class StoreIdConverter : JsonConverter<StoreId>
{
  public override StoreId? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    string? value = reader.GetString();

    return value == null ? null : new StoreId(value);
  }

  public override void Write(Utf8JsonWriter writer, StoreId bannerId, JsonSerializerOptions options)
  {
    writer.WriteStringValue(bannerId?.Value);
  }
}
