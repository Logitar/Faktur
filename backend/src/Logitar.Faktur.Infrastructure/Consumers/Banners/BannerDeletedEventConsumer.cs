using Logitar.Faktur.Domain.Banners.Events;
using Logitar.Faktur.Infrastructure.Handlers;
using MassTransit;

namespace Logitar.Faktur.Infrastructure.Consumers.Banners;

internal class BannerDeletedEventConsumer : IConsumer<BannerDeletedEvent>
{
  private readonly IBannerEventHandler _handler;

  public BannerDeletedEventConsumer(IBannerEventHandler handler)
  {
    _handler = handler;
  }

  public async Task Consume(ConsumeContext<BannerDeletedEvent> context)
  {
    await _handler.HandleAsync(context.Message, context.CancellationToken);
  }
}
