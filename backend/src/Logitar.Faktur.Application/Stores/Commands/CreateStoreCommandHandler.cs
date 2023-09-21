using Logitar.Faktur.Application.Exceptions;
using Logitar.Faktur.Contracts;
using Logitar.Faktur.Contracts.Stores;
using Logitar.Faktur.Domain;
using Logitar.Faktur.Domain.Banners;
using Logitar.Faktur.Domain.Stores;
using MediatR;

namespace Logitar.Faktur.Application.Stores.Commands;

internal class CreateStoreCommandHandler : IRequestHandler<CreateStoreCommand, CommandResult>
{
  private readonly IApplicationContext _applicationContext;
  private readonly IBannerRepository _bannerRepository;
  private readonly IStoreRepository _storeRepository;

  public CreateStoreCommandHandler(IApplicationContext applicationContext, IBannerRepository bannerRepository, IStoreRepository storeRepository)
  {
    _applicationContext = applicationContext;
    _bannerRepository = bannerRepository;
    _storeRepository = storeRepository;
  }

  public async Task<CommandResult> Handle(CreateStoreCommand command, CancellationToken cancellationToken)
  {
    CreateStorePayload payload = command.Payload;

    StoreId? id = null;
    if (!string.IsNullOrWhiteSpace(payload.Id))
    {
      id = new(payload.Id);
      if (await _storeRepository.LoadAsync(id, cancellationToken) != null)
      {
        throw new IdentifierAlreadyUsedException<StoreAggregate>(payload.Id, nameof(payload.Id));
      }
    }

    BannerAggregate? banner = null;
    if (!string.IsNullOrWhiteSpace(payload.BannerId))
    {
      BannerId bannerId = new(payload.BannerId);
      banner = await _bannerRepository.LoadAsync(bannerId, cancellationToken)
        ?? throw new AggregateNotFoundException<BannerAggregate>(bannerId.AggregateId, nameof(payload.BannerId));
    }

    DisplayName displayName = new(payload.DisplayName);
    StoreAggregate store = new(displayName, _applicationContext.ActorId, id)
    {
      Number = StoreNumber.TryCreate(payload.Number),
      Description = Description.TryCreate(payload.Description),
      Address = ReadOnlyAddress.TryCreate(payload.Address),
      Phone = ReadOnlyPhone.TryCreate(payload.Phone)
    };
    store.SetBanner(banner);

    store.Update(_applicationContext.ActorId);

    await _storeRepository.SaveAsync(store, cancellationToken);

    return _applicationContext.CreateCommandResult(store);
  }
}
