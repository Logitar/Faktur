using Logitar.Faktur.Application.Exceptions;
using Logitar.Faktur.Contracts;
using Logitar.Faktur.Contracts.Departments;
using Logitar.Faktur.Domain;
using Logitar.Faktur.Domain.Departments;
using Logitar.Faktur.Domain.Stores;
using MediatR;

namespace Logitar.Faktur.Application.Departments.Commands;

internal class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, CommandResult>
{
  private readonly IApplicationContext _applicationContext;
  private readonly IStoreRepository _storeRepository;

  public UpdateDepartmentCommandHandler(IApplicationContext applicationContext, IStoreRepository storeRepository)
  {
    _applicationContext = applicationContext;
    _storeRepository = storeRepository;
  }

  public async Task<CommandResult> Handle(UpdateDepartmentCommand command, CancellationToken cancellationToken)
  {
    StoreId storeId = new(command.StoreId);
    StoreAggregate store = await _storeRepository.LoadAsync(storeId, cancellationToken)
      ?? throw new AggregateNotFoundException<StoreAggregate>(storeId.AggregateId, nameof(command.StoreId));

    UpdateDepartmentPayload payload = command.Payload;

    DepartmentNumber number = new(command.Number);
    if (!store.Departments.TryGetValue(number, out ReadOnlyDepartment? existingDepartment))
    {
      throw new DepartmentNotFoundException(storeId, number, nameof(command.Number));
    }

    DisplayName displayName = string.IsNullOrWhiteSpace(payload.DisplayName)
      ? existingDepartment.DisplayName : new(payload.DisplayName);
    Description? description = payload.Description == null
      ? existingDepartment.Description : Description.TryCreate(payload.Description.Value);

    ReadOnlyDepartment department = new(displayName, description);
    store.SetDepartment(number, department);

    store.Update(_applicationContext.ActorId);

    await _storeRepository.SaveAsync(store, cancellationToken);

    return _applicationContext.CreateCommandResult(store);
  }
}
