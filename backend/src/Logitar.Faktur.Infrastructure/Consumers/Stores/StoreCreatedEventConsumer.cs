using Logitar.Faktur.Domain.Stores.Events;
using Logitar.Faktur.Infrastructure.Handlers;
using MassTransit;

namespace Logitar.Faktur.Infrastructure.Consumers.Stores;

internal class StoreCreatedEventConsumer : IConsumer<StoreCreatedEvent>
{
  private readonly IStoreEventHandler _handler;

  public StoreCreatedEventConsumer(IStoreEventHandler handler)
  {
    _handler = handler;
  }

  public async Task Consume(ConsumeContext<StoreCreatedEvent> context)
  {
    await _handler.HandleAsync(context.Message, context.CancellationToken);
  }
}
