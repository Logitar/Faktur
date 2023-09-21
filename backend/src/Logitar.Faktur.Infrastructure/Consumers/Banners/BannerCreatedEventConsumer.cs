using Logitar.Faktur.Domain.Banners.Events;
using Logitar.Faktur.Infrastructure.Handlers;
using MassTransit;

namespace Logitar.Faktur.Infrastructure.Consumers.Banners;

internal class BannerCreatedEventConsumer : IConsumer<BannerCreatedEvent>
{
  private readonly IBannerEventHandler _handler;

  public BannerCreatedEventConsumer(IBannerEventHandler handler)
  {
    _handler = handler;
  }

  public async Task Consume(ConsumeContext<BannerCreatedEvent> context)
  {
    await _handler.HandleAsync(context.Message, context.CancellationToken);
  }
}
