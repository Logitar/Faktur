using Logitar.Faktur.Domain.Banners.Events;
using Logitar.Faktur.Infrastructure.Handlers;
using MassTransit;

namespace Logitar.Faktur.Infrastructure.Consumers.Banners;

internal class BannerUpdatedEventConsumer : IConsumer<BannerUpdatedEvent>
{
  private readonly IBannerEventHandler _handler;

  public BannerUpdatedEventConsumer(IBannerEventHandler handler)
  {
    _handler = handler;
  }

  public async Task Consume(ConsumeContext<BannerUpdatedEvent> context)
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
