using Logitar.Faktur.Domain.Departments.Events;

namespace Logitar.Faktur.Infrastructure.Handlers;

public interface IDepartmentEventHandler
{
  Task<bool> HandleAsync(DepartmentRemovedEvent @event, CancellationToken cancellationToken = default);
  Task<bool> HandleAsync(DepartmentSavedEvent @event, CancellationToken cancellationToken = default);
}
