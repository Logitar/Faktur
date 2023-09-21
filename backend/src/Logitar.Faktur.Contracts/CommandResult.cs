using Logitar.Faktur.Contracts.Actors;

namespace Logitar.Faktur.Contracts;

public record CommandResult
{
  public string Id { get; set; } = string.Empty;
  public long Version { get; set; }

  public Actor Actor { get; set; } = new();
  public DateTime Timestamp { get; set; }
}
