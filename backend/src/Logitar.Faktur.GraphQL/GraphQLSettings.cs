namespace Logitar.Faktur.GraphQL;

public record GraphQLSettings
{
  public bool EnableMetrics { get; init; }
  public bool ExposeExceptionDetails { get; init; }
}
