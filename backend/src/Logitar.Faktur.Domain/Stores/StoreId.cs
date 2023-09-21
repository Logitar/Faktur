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
