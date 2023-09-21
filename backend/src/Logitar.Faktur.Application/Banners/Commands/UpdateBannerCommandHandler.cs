using Logitar.Faktur.Application.Exceptions;
using Logitar.Faktur.Contracts;
using Logitar.Faktur.Contracts.Banners;
using Logitar.Faktur.Domain;
using Logitar.Faktur.Domain.Banners;
using MediatR;

namespace Logitar.Faktur.Application.Banners.Commands;

internal class UpdateBannerCommandHandler : IRequestHandler<UpdateBannerCommand, CommandResult>
{
  private readonly IApplicationContext _applicationContext;
  private readonly IBannerRepository _bannerRepository;

  public UpdateBannerCommandHandler(IApplicationContext applicationContext, IBannerRepository bannerRepository)
  {
    _applicationContext = applicationContext;
    _bannerRepository = bannerRepository;
  }

  public async Task<CommandResult> Handle(UpdateBannerCommand command, CancellationToken cancellationToken)
  {
    BannerId id = new(command.Id);
    BannerAggregate banner = await _bannerRepository.LoadAsync(id, cancellationToken)
      ?? throw new AggregateNotFoundException<BannerAggregate>(id.AggregateId, nameof(command.Id));

    UpdateBannerPayload payload = command.Payload;

    if (!string.IsNullOrWhiteSpace(payload.DisplayName))
    {
      banner.DisplayName = new DisplayName(payload.DisplayName);
    }
    if (payload.Description != null)
    {
      banner.Description = Description.TryCreate(payload.Description.Value);
    }

    banner.Update(_applicationContext.ActorId);

    await _bannerRepository.SaveAsync(banner, cancellationToken);

    return _applicationContext.CreateCommandResult(banner);
  }
}
