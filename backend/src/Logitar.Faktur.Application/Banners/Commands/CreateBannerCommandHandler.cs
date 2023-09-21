using Logitar.Faktur.Application.Exceptions;
using Logitar.Faktur.Contracts;
using Logitar.Faktur.Contracts.Banners;
using Logitar.Faktur.Domain;
using Logitar.Faktur.Domain.Banners;
using MediatR;

namespace Logitar.Faktur.Application.Banners.Commands;

internal class CreateBannerCommandHandler : IRequestHandler<CreateBannerCommand, CommandResult>
{
  private readonly IApplicationContext _applicationContext;
  private readonly IBannerRepository _bannerRepository;

  public CreateBannerCommandHandler(IApplicationContext applicationContext, IBannerRepository bannerRepository)
  {
    _applicationContext = applicationContext;
    _bannerRepository = bannerRepository;
  }

  public async Task<CommandResult> Handle(CreateBannerCommand command, CancellationToken cancellationToken)
  {
    CreateBannerPayload payload = command.Payload;

    BannerId? id = null;
    if (!string.IsNullOrWhiteSpace(payload.Id))
    {
      id = new(payload.Id);
      if (await _bannerRepository.LoadAsync(id, cancellationToken) != null)
      {
        throw new IdentifierAlreadyUsedException<BannerAggregate>(payload.Id, nameof(payload.Id));
      }
    }

    DisplayName displayName = new(payload.DisplayName);
    BannerAggregate banner = new(displayName, _applicationContext.ActorId, id)
    {
      Description = Description.TryCreate(payload.Description)
    };

    banner.Update(_applicationContext.ActorId);

    await _bannerRepository.SaveAsync(banner, cancellationToken);

    return _applicationContext.CreateCommandResult(banner);
  }
}
