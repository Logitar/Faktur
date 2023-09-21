using Logitar.Faktur.Domain.Banners.Events;
using Logitar.Faktur.EntityFrameworkCore.Relational.Entities;
using Logitar.Faktur.Infrastructure.Handlers;
using Microsoft.EntityFrameworkCore;

namespace Logitar.Faktur.EntityFrameworkCore.Relational.Handlers;

internal class BannerEventHandler : IBannerEventHandler
{
  private readonly FakturContext _context;

  public BannerEventHandler(FakturContext context)
  {
    _context = context;
  }

  public async Task HandleAsync(BannerCreatedEvent @event, CancellationToken cancellationToken)
  {
    BannerEntity? banner = await _context.Banners.AsNoTracking()
      .SingleOrDefaultAsync(x => x.AggregateId == @event.AggregateId.Value, cancellationToken);

    if (banner == null)
    {
      banner = new(@event);
      _context.Banners.Add(banner);

      await _context.SaveChangesAsync(cancellationToken);
    }
  }

  public async Task HandleAsync(BannerDeletedEvent @event, CancellationToken cancellationToken)
  {
    BannerEntity? banner = await _context.Banners
      .SingleOrDefaultAsync(x => x.AggregateId == @event.AggregateId.Value, cancellationToken);

    if (banner != null)
    {
      _context.Banners.Remove(banner);

      await _context.SaveChangesAsync(cancellationToken);
    }
  }

  public async Task<bool> HandleAsync(BannerUpdatedEvent @event, CancellationToken cancellationToken)
  {
    BannerEntity? banner = await _context.Banners
      .SingleOrDefaultAsync(x => x.AggregateId == @event.AggregateId.Value, cancellationToken);

    long expectedVersion = @event.Version - 1;

    if (banner == null || banner.Version < expectedVersion)
    {
      return false;
    }

    if (banner.Version == expectedVersion)
    {
      banner.Update(@event);

      await _context.SaveChangesAsync(cancellationToken);
    }

    return true;
  }
}
