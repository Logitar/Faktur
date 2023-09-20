using Logitar.Faktur.Domain.Banners.Events;
using Logitar.Faktur.EntityFrameworkCore.Relational.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Logitar.Faktur.EntityFrameworkCore.Relational.Handlers.Banners;

internal class BannerUpdatedEventConsumer : IConsumer<BannerUpdatedEvent>
{
  private readonly FakturContext _context;

  public BannerUpdatedEventConsumer(FakturContext context)
  {
    _context = context;
  }

  public async Task Consume(ConsumeContext<BannerUpdatedEvent> context)
  {
    BannerUpdatedEvent @event = context.Message;
    CancellationToken cancellationToken = context.CancellationToken;

    BannerEntity? banner = await _context.Banners
      .SingleOrDefaultAsync(x => x.AggregateId == @event.AggregateId.Value, cancellationToken);

    if (banner == null)
    {
      throw new NotImplementedException(); // TODO(fpion): load events up to @event.Version - 1 and reconstruct banner?
    }

    banner.Update(@event);

    await _context.SaveChangesAsync(cancellationToken);
  }
}
