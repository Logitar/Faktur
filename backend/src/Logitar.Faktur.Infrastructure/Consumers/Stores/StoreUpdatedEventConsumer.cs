using Logitar.Faktur.Domain.Stores.Events;
using Logitar.Faktur.Infrastructure.Handlers;
using MassTransit;

namespace Logitar.Faktur.Infrastructure.Consumers.Stores;

internal class StoreUpdatedEventConsumer : IConsumer<StoreUpdatedEvent>
{
  private readonly IStoreEventHandler _handler;

  public StoreUpdatedEventConsumer(IStoreEventHandler handler)
  {
    _handler = handler;
  }

  public async Task Consume(ConsumeContext<StoreUpdatedEvent> context)
  {
    IEnumerable<TimeSpan> delays = new[] { TimeSpan.Zero }.Concat(RetryHelper.CreateDelays());
    foreach (TimeSpan delay in delays)
    {
      if (delay > TimeSpan.Zero)
      {
        Thread.Sleep(delay);
      }

      if (await _handler.HandleAsync(context.Message, context.CancellationToken))
      {
        break;
      }
    }
  }
}
