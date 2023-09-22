using Logitar.Faktur.Domain.Departments.Events;
using Logitar.Faktur.EntityFrameworkCore.Relational.Entities;
using Logitar.Faktur.Infrastructure.Handlers;
using Microsoft.EntityFrameworkCore;

namespace Logitar.Faktur.EntityFrameworkCore.Relational.Handlers;

internal class DepartmentEventHandler : IDepartmentEventHandler
{
  private readonly FakturContext _context;

  public DepartmentEventHandler(FakturContext context)
  {
    _context = context;
  }

  public async Task<bool> HandleAsync(DepartmentRemovedEvent @event, CancellationToken cancellationToken)
  {
    StoreEntity? store = await _context.Stores
      .Include(x => x.Departments)
      .SingleOrDefaultAsync(x => x.AggregateId == @event.AggregateId.Value, cancellationToken);

    long expectedVersion = @event.Version - 1;

    if (store == null || store.Version < expectedVersion)
    {
      return false;
    }

    if (store.Version == expectedVersion)
    {
      store.RemoveDepartment(@event);

      await _context.SaveChangesAsync(cancellationToken);
    }

    return true;
  }

  public async Task<bool> HandleAsync(DepartmentSavedEvent @event, CancellationToken cancellationToken)
  {
    StoreEntity? store = await _context.Stores
      .Include(x => x.Departments)
      .SingleOrDefaultAsync(x => x.AggregateId == @event.AggregateId.Value, cancellationToken);

    long expectedVersion = @event.Version - 1;

    if (store == null || store.Version < expectedVersion)
    {
      return false;
    }

    if (store.Version == expectedVersion)
    {
      store.SetDepartment(@event);

      await _context.SaveChangesAsync(cancellationToken);
    }

    return true;
  }
}
