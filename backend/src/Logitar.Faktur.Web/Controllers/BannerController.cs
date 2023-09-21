using Logitar.Faktur.Contracts;
using Logitar.Faktur.Contracts.Banners;
using Logitar.Faktur.Contracts.Search;
using Logitar.Faktur.Web.Models.Banners;
using Microsoft.AspNetCore.Mvc;

namespace Logitar.Faktur.Web.Controllers;

[ApiController]
[Route("banners")]
public class BannerController : ControllerBase // TODO(fpion): Authorization
{
  private readonly IBannerService _bannerService;

  public BannerController(IBannerService bannerService)
  {
    _bannerService = bannerService;
  }

  [HttpPost]
  public async Task<ActionResult<CommandResult>> CreateAsync([FromBody] CreateBannerPayload payload, CancellationToken cancellationToken)
  {
    CommandResult result = await _bannerService.CreateAsync(payload, cancellationToken);
    Uri uri = new($"{Request.Scheme}://{Request.Host}/banners/{result.Id}");

    return Accepted(uri, result);
  }

  [HttpDelete("{id}")]
  public async Task<ActionResult<CommandResult>> DeleteAsync(string id, CancellationToken cancellationToken)
  {
    return Accepted(await _bannerService.DeleteAsync(id, cancellationToken));
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Banner>> ReadAsync(string id, CancellationToken cancellationToken)
  {
    Banner? banner = await _bannerService.ReadAsync(id, cancellationToken);
    return banner == null ? NotFound() : Ok(banner);
  }

  [HttpPut("{id}")]
  public async Task<ActionResult<CommandResult>> ReplaceAsync(string id, [FromBody] ReplaceBannerPayload payload, CancellationToken cancellationToken)
  {
    return Accepted(await _bannerService.ReplaceAsync(id, payload, cancellationToken));
  }

  [HttpGet]
  public async Task<ActionResult<SearchResults<Banner>>> SearchAsync([FromQuery] SearchBannersQuery query, CancellationToken cancellationToken)
  {
    return Ok(await _bannerService.SearchAsync(query.ToPayload(), cancellationToken));
  }

  [HttpPatch("{id}")]
  public async Task<ActionResult<CommandResult>> UpdateAsync(string id, [FromBody] UpdateBannerPayload payload, CancellationToken cancellationToken)
  {
    return Accepted(await _bannerService.UpdateAsync(id, payload, cancellationToken));
  }
}
