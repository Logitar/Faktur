using Logitar.EventSourcing;
using Logitar.Faktur.Contracts;

namespace Logitar.Faktur.Application;

internal static class ApplicationContextExtensions
{
  public static CommandResult CreateCommandResult(this IApplicationContext applicationContext, AggregateRoot aggregate)
  {
    return new CommandResult
    {
      Id = aggregate.Id.Format(),
      Version = aggregate.Version,
      Actor = applicationContext.Actor,
      Timestamp = aggregate.UpdatedOn
    };
  }
}
