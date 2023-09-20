using Logitar.Faktur.Domain.Banners.Events;
using Logitar.Faktur.EntityFrameworkCore.Relational.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Logitar.Faktur.EntityFrameworkCore.Relational.Handlers.Banners;

internal class BannerCreatedEventConsumer : IConsumer<BannerCreatedEvent>
{
  private readonly FakturContext _context;

  public BannerCreatedEventConsumer(FakturContext context)
  {
    _context = context;
  }

  public async Task Consume(ConsumeContext<BannerCreatedEvent> context)
  {
    BannerCreatedEvent @event = context.Message;
    CancellationToken cancellationToken = context.CancellationToken;

    BannerEntity? banner = await _context.Banners.AsNoTracking()
      .SingleOrDefaultAsync(x => x.AggregateId == @event.AggregateId.Value, cancellationToken);

    if (banner == null)
    {
      banner = new(@event);
      _context.Banners.Add(banner);

      await _context.SaveChangesAsync(cancellationToken);
    }
  }
}
