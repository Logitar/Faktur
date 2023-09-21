using Logitar.Faktur.Contracts.Search;
using Logitar.Faktur.Contracts.Stores;
using MediatR;

namespace Logitar.Faktur.Application.Stores.Queries;

internal class SearchStoresQueryHandler : IRequestHandler<SearchStoresQuery, SearchResults<Store>>
{
  private readonly IStoreQuerier _storeQuerier;

  public SearchStoresQueryHandler(IStoreQuerier storeQuerier)
  {
    _storeQuerier = storeQuerier;
  }

  public async Task<SearchResults<Store>> Handle(SearchStoresQuery query, CancellationToken cancellationToken)
  {
    return await _storeQuerier.SearchAsync(query.Payload, cancellationToken);
  }
}
