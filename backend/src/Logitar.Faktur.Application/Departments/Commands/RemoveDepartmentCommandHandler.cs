using Logitar.Faktur.Application.Exceptions;
using Logitar.Faktur.Contracts;
using Logitar.Faktur.Domain.Departments;
using Logitar.Faktur.Domain.Stores;
using MediatR;

namespace Logitar.Faktur.Application.Departments.Commands;

internal class RemoveDepartmentCommandHandler : IRequestHandler<RemoveDepartmentCommand, CommandResult>
{
  private readonly IApplicationContext _applicationContext;
  private readonly IStoreRepository _storeRepository;

  public RemoveDepartmentCommandHandler(IApplicationContext applicationContext, IStoreRepository storeRepository)
  {
    _applicationContext = applicationContext;
    _storeRepository = storeRepository;
  }

  public async Task<CommandResult> Handle(RemoveDepartmentCommand command, CancellationToken cancellationToken)
  {
    StoreId storeId = new(command.StoreId);
    StoreAggregate store = await _storeRepository.LoadAsync(storeId, cancellationToken)
      ?? throw new AggregateNotFoundException<StoreAggregate>(storeId.AggregateId, nameof(command.StoreId));

    store.RemoveDepartment(new DepartmentNumber(command.Number));

    store.Update(_applicationContext.ActorId);

    await _storeRepository.SaveAsync(store, cancellationToken);

    return _applicationContext.CreateCommandResult(store);
  }
}
