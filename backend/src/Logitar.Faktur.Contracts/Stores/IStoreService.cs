using Logitar.Faktur.Contracts.Search;

namespace Logitar.Faktur.Contracts.Stores;

public interface IStoreService
{
  Task<CommandResult> CreateAsync(CreateStorePayload payload, CancellationToken cancellationToken = default);
  Task<CommandResult> DeleteAsync(string id, CancellationToken cancellationToken = default);
  Task<Store?> ReadAsync(string id, CancellationToken cancellationToken = default);
  Task<CommandResult> ReplaceAsync(string id, ReplaceStorePayload payload, CancellationToken cancellationToken = default);
  Task<SearchResults<Store>> SearchAsync(SearchStoresPayload payload, CancellationToken cancellationToken = default);
  Task<CommandResult> UpdateAsync(string id, UpdateStorePayload payload, CancellationToken cancellationToken = default);
}
