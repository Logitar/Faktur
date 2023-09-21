using Logitar.Faktur.Domain.Stores.Events;
using Logitar.Faktur.EntityFrameworkCore.Relational.Entities;
using Logitar.Faktur.Infrastructure.Handlers;
using Microsoft.EntityFrameworkCore;

namespace Logitar.Faktur.EntityFrameworkCore.Relational.Handlers;

internal class StoreEventHandler : IStoreEventHandler
{
  private readonly FakturContext _context;

  public StoreEventHandler(FakturContext context)
  {
    _context = context;
  }

  public async Task HandleAsync(StoreCreatedEvent @event, CancellationToken cancellationToken)
  {
    StoreEntity? store = await _context.Stores.AsNoTracking()
      .SingleOrDefaultAsync(x => x.AggregateId == @event.AggregateId.Value, cancellationToken);

    if (store == null)
    {
      store = new(@event);
      _context.Stores.Add(store);

      await _context.SaveChangesAsync(cancellationToken);
    }
  }

  public async Task HandleAsync(StoreDeletedEvent @event, CancellationToken cancellationToken)
  {
    StoreEntity? store = await _context.Stores
      .SingleOrDefaultAsync(x => x.AggregateId == @event.AggregateId.Value, cancellationToken);

    if (store != null)
    {
      _context.Stores.Remove(store);

      await _context.SaveChangesAsync(cancellationToken);
    }
  }

  public async Task<bool> HandleAsync(StoreUpdatedEvent @event, CancellationToken cancellationToken)
  {
    StoreEntity? store = await _context.Stores
      .SingleOrDefaultAsync(x => x.AggregateId == @event.AggregateId.Value, cancellationToken);

    long expectedVersion = @event.Version - 1;

    if (store == null || store.Version < expectedVersion)
    {
      return false;
    }

    if (store.Version == expectedVersion)
    {
      BannerEntity? banner = @event.BannerId?.Value != null
        ? await _context.Banners.SingleOrDefaultAsync(x => x.AggregateId == @event.BannerId.Value.Value, cancellationToken)
        : null;

      store.Update(@event, banner);

      await _context.SaveChangesAsync(cancellationToken);
    }

    return true;
  }
}
