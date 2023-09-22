using Logitar.Faktur.Application.Exceptions;
using Logitar.Faktur.Contracts;
using Logitar.Faktur.Domain.Banners;
using MediatR;

namespace Logitar.Faktur.Application.Banners.Commands;

internal class DeleteBannerCommandHandler : IRequestHandler<DeleteBannerCommand, CommandResult>
{
  private readonly IApplicationContext _applicationContext;
  private readonly IBannerRepository _bannerRepository;

  public DeleteBannerCommandHandler(IApplicationContext applicationContext, IBannerRepository bannerRepository)
  {
    _applicationContext = applicationContext;
    _bannerRepository = bannerRepository;
  }

  public async Task<CommandResult> Handle(DeleteBannerCommand command, CancellationToken cancellationToken)
  {
    BannerId id = new(command.Id);
    BannerAggregate banner = await _bannerRepository.LoadAsync(id, cancellationToken)
      ?? throw new AggregateNotFoundException<BannerAggregate>(id.AggregateId, nameof(command.Id));

    banner.Delete(_applicationContext.ActorId);

    // TODO(fpion): remove the banner from its store

    await _bannerRepository.SaveAsync(banner, cancellationToken);

    return _applicationContext.CreateCommandResult(banner);
  }
}
