using Logitar.Faktur.Domain.Banners.Events;
using Logitar.Faktur.EntityFrameworkCore.Relational.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Logitar.Faktur.EntityFrameworkCore.Relational.Handlers.Banners;

internal class BannerDeletedEventConsumer : IConsumer<BannerDeletedEvent>
{
  private readonly FakturContext _context;

  public BannerDeletedEventConsumer(FakturContext context)
  {
    _context = context;
  }

  public async Task Consume(ConsumeContext<BannerDeletedEvent> context)
  {
    BannerDeletedEvent @event = context.Message;
    CancellationToken cancellationToken = context.CancellationToken;

    BannerEntity? banner = await _context.Banners
      .SingleOrDefaultAsync(x => x.AggregateId == @event.AggregateId.Value, cancellationToken);

    if (banner != null)
    {
      _context.Banners.Remove(banner);

      await _context.SaveChangesAsync(cancellationToken);
    }
  }
}
