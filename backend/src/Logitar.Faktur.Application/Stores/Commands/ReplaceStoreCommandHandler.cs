using Logitar.Faktur.Application.Exceptions;
using Logitar.Faktur.Contracts;
using Logitar.Faktur.Contracts.Stores;
using Logitar.Faktur.Domain;
using Logitar.Faktur.Domain.Banners;
using Logitar.Faktur.Domain.Stores;
using MediatR;

namespace Logitar.Faktur.Application.Stores.Commands;

internal class ReplaceStoreCommandHandler : IRequestHandler<ReplaceStoreCommand, CommandResult>
{
  private readonly IApplicationContext _applicationContext;
  private readonly IBannerRepository _bannerRepository;
  private readonly IStoreRepository _storeRepository;

  public ReplaceStoreCommandHandler(IApplicationContext applicationContext, IBannerRepository bannerRepository, IStoreRepository storeRepository)
  {
    _applicationContext = applicationContext;
    _bannerRepository = bannerRepository;
    _storeRepository = storeRepository;
  }

  public async Task<CommandResult> Handle(ReplaceStoreCommand command, CancellationToken cancellationToken)
  {
    StoreId id = new(command.Id);
    StoreAggregate store = await _storeRepository.LoadAsync(id, cancellationToken)
      ?? throw new AggregateNotFoundException<StoreAggregate>(id.AggregateId, nameof(command.Id));

    ReplaceStorePayload payload = command.Payload;

    BannerAggregate? banner = null;
    if (!string.IsNullOrWhiteSpace(payload.BannerId))
    {
      BannerId bannerId = new(payload.BannerId);
      banner = await _bannerRepository.LoadAsync(bannerId, cancellationToken)
        ?? throw new AggregateNotFoundException<BannerAggregate>(bannerId.AggregateId, nameof(payload.BannerId));
    }

    store.SetBanner(banner);

    store.Number = StoreNumber.TryCreate(payload.Number);
    store.DisplayName = new DisplayName(payload.DisplayName);
    store.Description = Description.TryCreate(payload.Description);

    store.Address = ReadOnlyAddress.TryCreate(payload.Address);
    store.Phone = ReadOnlyPhone.TryCreate(payload.Phone);

    store.Update(_applicationContext.ActorId);

    await _storeRepository.SaveAsync(store, cancellationToken);

    return _applicationContext.CreateCommandResult(store);
  }
}
