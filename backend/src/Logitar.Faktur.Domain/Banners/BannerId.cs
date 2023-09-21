using Logitar.EventSourcing;

namespace Logitar.Faktur.Domain.Banners;

public record BannerId
{
  public AggregateId AggregateId { get; }
  public string Value => AggregateId.Value;

  public BannerId(string value)
    : this(Guid.TryParse(value.Trim(), out Guid guid) ? new AggregateId(guid) : new(value))
  {
  }
  public BannerId(AggregateId aggregateId)
  {
    AggregateId = aggregateId;
  }
}

public class BannerIdConverter : JsonConverter<BannerId>
{
  public override BannerId? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    string? value = reader.GetString();

    return value == null ? null : new BannerId(value);
  }

  public override void Write(Utf8JsonWriter writer, BannerId bannerId, JsonSerializerOptions options)
  {
    writer.WriteStringValue(bannerId?.Value);
  }
}
