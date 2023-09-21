namespace Logitar.Faktur.Domain.Stores;

public interface IStoreRepository
{
  Task<StoreAggregate?> LoadAsync(StoreId id, CancellationToken cancellationToken = default);
  Task SaveAsync(StoreAggregate store, CancellationToken cancellationToken = default);
}
