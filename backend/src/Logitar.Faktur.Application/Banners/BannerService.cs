using Logitar.Faktur.Application.Banners.Commands;
using Logitar.Faktur.Contracts;
using Logitar.Faktur.Contracts.Banners;
using Logitar.Faktur.Contracts.Search;
using MediatR;

namespace Logitar.Faktur.Application.Banners;

internal class BannerService : IBannerService
{
  private readonly IMediator _mediator;

  public BannerService(IMediator mediator)
  {
    _mediator = mediator;
  }

  public async Task<CommandResult> CreateAsync(CreateBannerPayload payload, CancellationToken cancellationToken)
  {
    return await _mediator.Send(new CreateBannerCommand(payload), cancellationToken);
  }

  public Task<CommandResult> DeleteAsync(string id, CancellationToken cancellationToken)
  {
    throw new NotImplementedException();
  }

  public Task<Banner?> ReadAsync(string id, CancellationToken cancellationToken)
  {
    throw new NotImplementedException();
  }

  public Task<CommandResult> ReplaceAsync(string id, ReplaceBannerPayload payload, CancellationToken cancellationToken)
  {
    throw new NotImplementedException();
  }

  public Task<SearchResults<Banner>> SearchAsync(SearchBannersPayload payload, CancellationToken cancellationToken)
  {
    throw new NotImplementedException();
  }

  public Task<CommandResult> UpdateAsync(string id, UpdateBannerPayload payload, CancellationToken cancellationToken)
  {
    throw new NotImplementedException();
  }
}
