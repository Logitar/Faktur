using Logitar.Faktur.Application.Departments.Commands;
using Logitar.Faktur.Application.Departments.Queries;
using Logitar.Faktur.Contracts;
using Logitar.Faktur.Contracts.Departments;
using Logitar.Faktur.Contracts.Search;
using MediatR;

namespace Logitar.Faktur.Application.Departments;

internal class DepartmentService : IDepartmentService
{
  private readonly IMediator _mediator;

  public DepartmentService(IMediator mediator)
  {
    _mediator = mediator;
  }

  public async Task<Department?> ReadAsync(string storeId, string departmentNumber, CancellationToken cancellationToken)
  {
    return await _mediator.Send(new ReadDepartmentQuery(storeId, departmentNumber), cancellationToken);
  }

  public async Task<CommandResult> RemoveAsync(string storeId, string departmentNumber, CancellationToken cancellationToken)
  {
    return await _mediator.Send(new RemoveDepartmentCommand(storeId, departmentNumber), cancellationToken);
  }

  public async Task<CommandResult> SaveAsync(string storeId, string departmentNumber, SaveDepartmentPayload payload, CancellationToken cancellationToken)
  {
    return await _mediator.Send(new SaveDepartmentCommand(storeId, departmentNumber, payload), cancellationToken);
  }

  public async Task<SearchResults<Department>> SearchAsync(string storeId, SearchDepartmentsPayload payload, CancellationToken cancellationToken)
  {
    return await _mediator.Send(new SearchDepartmentsQuery(storeId, payload), cancellationToken);
  }

  public async Task<CommandResult> UpdateAsync(string storeId, string departmentNumber, UpdateDepartmentPayload payload, CancellationToken cancellationToken)
  {
    return await _mediator.Send(new UpdateDepartmentCommand(storeId, departmentNumber, payload), cancellationToken);
  }
}
