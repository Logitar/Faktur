using Logitar.Faktur.Application.Exceptions;
using Logitar.Faktur.Contracts;
using Logitar.Faktur.Domain.Stores;
using MediatR;

namespace Logitar.Faktur.Application.Stores.Commands;

internal class DeleteStoreCommandHandler : IRequestHandler<DeleteStoreCommand, CommandResult>
{
  private readonly IApplicationContext _applicationContext;
  private readonly IStoreRepository _storeRepository;

  public DeleteStoreCommandHandler(IApplicationContext applicationContext, IStoreRepository storeRepository)
  {
    _applicationContext = applicationContext;
    _storeRepository = storeRepository;
  }

  public async Task<CommandResult> Handle(DeleteStoreCommand command, CancellationToken cancellationToken)
  {
    StoreId id = new(command.Id);
    StoreAggregate store = await _storeRepository.LoadAsync(id, cancellationToken)
      ?? throw new AggregateNotFoundException<StoreAggregate>(id.AggregateId, nameof(command.Id));

    store.Delete(_applicationContext.ActorId);

    await _storeRepository.SaveAsync(store, cancellationToken);

    return _applicationContext.CreateCommandResult(store);
  }
}
