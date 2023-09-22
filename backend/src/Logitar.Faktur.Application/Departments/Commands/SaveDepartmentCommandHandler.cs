using Logitar.Faktur.Application.Exceptions;
using Logitar.Faktur.Contracts;
using Logitar.Faktur.Contracts.Departments;
using Logitar.Faktur.Domain;
using Logitar.Faktur.Domain.Departments;
using Logitar.Faktur.Domain.Stores;
using MediatR;

namespace Logitar.Faktur.Application.Departments.Commands;

internal class SaveDepartmentCommandHandler : IRequestHandler<SaveDepartmentCommand, CommandResult>
{
  private readonly IApplicationContext _applicationContext;
  private readonly IStoreRepository _storeRepository;

  public SaveDepartmentCommandHandler(IApplicationContext applicationContext, IStoreRepository storeRepository)
  {
    _applicationContext = applicationContext;
    _storeRepository = storeRepository;
  }

  public async Task<CommandResult> Handle(SaveDepartmentCommand command, CancellationToken cancellationToken)
  {
    StoreId storeId = new(command.StoreId);
    StoreAggregate store = await _storeRepository.LoadAsync(storeId, cancellationToken)
      ?? throw new AggregateNotFoundException<StoreAggregate>(storeId.AggregateId, nameof(command.StoreId));

    SaveDepartmentPayload payload = command.Payload;

    DepartmentNumber number = new(command.Number);
    ReadOnlyDepartment department = new(new DisplayName(payload.DisplayName), Description.TryCreate(payload.Description));
    store.SetDepartment(number, department);

    store.Update(_applicationContext.ActorId);

    await _storeRepository.SaveAsync(store, cancellationToken);

    return _applicationContext.CreateCommandResult(store);
  }
}
