using Logitar.Faktur.Application.Stores.Commands;
using Logitar.Faktur.Application.Stores.Queries;
using Logitar.Faktur.Contracts;
using Logitar.Faktur.Contracts.Search;
using Logitar.Faktur.Contracts.Stores;
using MediatR;

namespace Logitar.Faktur.Application.Stores;

internal class StoreService : IStoreService
{
  private readonly IMediator _mediator;

  public StoreService(IMediator mediator)
  {
    _mediator = mediator;
  }

  public async Task<CommandResult> CreateAsync(CreateStorePayload payload, CancellationToken cancellationToken)
  {
    return await _mediator.Send(new CreateStoreCommand(payload), cancellationToken);
  }

  public async Task<CommandResult> DeleteAsync(string id, CancellationToken cancellationToken)
  {
    return await _mediator.Send(new DeleteStoreCommand(id), cancellationToken);
  }

  public async Task<Store?> ReadAsync(string id, CancellationToken cancellationToken)
  {
    return await _mediator.Send(new ReadStoreQuery(id), cancellationToken);
  }

  public async Task<CommandResult> ReplaceAsync(string id, ReplaceStorePayload payload, CancellationToken cancellationToken)
  {
    return await _mediator.Send(new ReplaceStoreCommand(id, payload), cancellationToken);
  }

  public async Task<SearchResults<Store>> SearchAsync(SearchStoresPayload payload, CancellationToken cancellationToken)
  {
    return await _mediator.Send(new SearchStoresQuery(payload), cancellationToken);
  }

  public async Task<CommandResult> UpdateAsync(string id, UpdateStorePayload payload, CancellationToken cancellationToken)
  {
    return await _mediator.Send(new UpdateStoreCommand(id, payload), cancellationToken);
  }
}
