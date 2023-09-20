using Logitar.EventSourcing;

namespace Logitar.Faktur.Domain.Banners;

public record BannerId
{
  public AggregateId AggregateId { get; }
  public string Value => AggregateId.Value;

  public BannerId(string value)
  {
    AggregateId = Guid.TryParse(value.Trim(), out Guid guid) ? new(guid) : new(value);
  }
}
