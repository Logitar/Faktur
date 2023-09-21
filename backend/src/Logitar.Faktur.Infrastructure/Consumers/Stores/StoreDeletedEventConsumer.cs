using Logitar.Faktur.Domain.Stores.Events;
using Logitar.Faktur.Infrastructure.Handlers;
using MassTransit;

namespace Logitar.Faktur.Infrastructure.Consumers.Stores;

internal class StoreDeletedEventConsumer : IConsumer<StoreDeletedEvent>
{
  private readonly IStoreEventHandler _handler;

  public StoreDeletedEventConsumer(IStoreEventHandler handler)
  {
    _handler = handler;
  }

  public async Task Consume(ConsumeContext<StoreDeletedEvent> context)
  {
    await _handler.HandleAsync(context.Message, context.CancellationToken);
  }
}
