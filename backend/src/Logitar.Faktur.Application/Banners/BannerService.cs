using Logitar.Faktur.Application.Banners.Commands;
using Logitar.Faktur.Application.Banners.Queries;
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

  public async Task<CommandResult> DeleteAsync(string id, CancellationToken cancellationToken)
  {
    return await _mediator.Send(new DeleteBannerCommand(id), cancellationToken);
  }

  public async Task<Banner?> ReadAsync(string id, CancellationToken cancellationToken)
  {
    return await _mediator.Send(new ReadBannerQuery(id), cancellationToken);
  }

  public async Task<CommandResult> ReplaceAsync(string id, ReplaceBannerPayload payload, CancellationToken cancellationToken)
  {
    return await _mediator.Send(new ReplaceBannerCommand(id, payload), cancellationToken);
  }

  public async Task<SearchResults<Banner>> SearchAsync(SearchBannersPayload payload, CancellationToken cancellationToken)
  {
    return await _mediator.Send(new SearchBannersQuery(payload), cancellationToken);
  }

  public async Task<CommandResult> UpdateAsync(string id, UpdateBannerPayload payload, CancellationToken cancellationToken)
  {
    return await _mediator.Send(new UpdateBannerCommand(id, payload), cancellationToken);
  }
}
