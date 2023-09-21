using Logitar.Faktur.Application.Exceptions;
using Logitar.Faktur.Contracts;
using Logitar.Faktur.Contracts.Stores;
using Logitar.Faktur.Domain;
using Logitar.Faktur.Domain.Banners;
using Logitar.Faktur.Domain.Stores;
using MediatR;

namespace Logitar.Faktur.Application.Stores.Commands;

internal class UpdateStoreCommandHandler : IRequestHandler<UpdateStoreCommand, CommandResult>
{
  private readonly IApplicationContext _applicationContext;
  private readonly IBannerRepository _bannerRepository;
  private readonly IStoreRepository _storeRepository;

  public UpdateStoreCommandHandler(IApplicationContext applicationContext, IBannerRepository bannerRepository, IStoreRepository storeRepository)
  {
    _applicationContext = applicationContext;
    _bannerRepository = bannerRepository;
    _storeRepository = storeRepository;
  }

  public async Task<CommandResult> Handle(UpdateStoreCommand command, CancellationToken cancellationToken)
  {
    StoreId id = new(command.Id);
    StoreAggregate store = await _storeRepository.LoadAsync(id, cancellationToken)
      ?? throw new AggregateNotFoundException<StoreAggregate>(id.AggregateId, nameof(command.Id));

    UpdateStorePayload payload = command.Payload;

    if (payload.BannerId != null)
    {
      BannerAggregate? banner = null;
      if (!string.IsNullOrWhiteSpace(payload.BannerId.Value))
      {
        BannerId bannerId = new(payload.BannerId.Value);
        banner = await _bannerRepository.LoadAsync(bannerId, cancellationToken)
          ?? throw new AggregateNotFoundException<BannerAggregate>(bannerId.AggregateId, nameof(payload.BannerId));
      }

      store.SetBanner(banner);
    }

    if (payload.Number != null)
    {
      store.Number = StoreNumber.TryCreate(payload.Number.Value);
    }
    if (!string.IsNullOrWhiteSpace(payload.DisplayName))
    {
      store.DisplayName = new DisplayName(payload.DisplayName);
    }
    if (payload.Description != null)
    {
      store.Description = Description.TryCreate(payload.Description.Value);
    }

    if (payload.Address != null)
    {
      store.Address = ReadOnlyAddress.TryCreate(payload.Address.Value);
    }
    if (payload.Phone != null)
    {
      store.Phone = ReadOnlyPhone.TryCreate(payload.Phone.Value);
    }

    store.Update(_applicationContext.ActorId);

    await _storeRepository.SaveAsync(store, cancellationToken);

    return _applicationContext.CreateCommandResult(store);
  }
}
