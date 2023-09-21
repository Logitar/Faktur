using Logitar.Faktur.Contracts;
using Logitar.Faktur.Contracts.Search;
using Logitar.Faktur.Contracts.Stores;
using Logitar.Faktur.Web.Models.Stores;
using Microsoft.AspNetCore.Mvc;

namespace Logitar.Faktur.Web.Controllers;

[ApiController]
[Route("stores")]
public class StoreController : ControllerBase // TODO(fpion): Authorization
{
  private readonly IStoreService _storeService;

  public StoreController(IStoreService storeService)
  {
    _storeService = storeService;
  }

  [HttpPost]
  public async Task<ActionResult<CommandResult>> CreateAsync([FromBody] CreateStorePayload payload, CancellationToken cancellationToken)
  {
    CommandResult result = await _storeService.CreateAsync(payload, cancellationToken);
    Uri uri = new($"{Request.Scheme}://{Request.Host}/stores/{result.Id}"); // TODO(fpion): refactor

    return Accepted(uri, result);
  }

  [HttpDelete("{id}")]
  public async Task<ActionResult<CommandResult>> DeleteAsync(string id, CancellationToken cancellationToken)
  {
    return Accepted(await _storeService.DeleteAsync(id, cancellationToken));
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Store>> ReadAsync(string id, CancellationToken cancellationToken)
  {
    Store? store = await _storeService.ReadAsync(id, cancellationToken);
    return store == null ? NotFound() : Ok(store);
  }

  [HttpPut("{id}")]
  public async Task<ActionResult<CommandResult>> ReplaceAsync(string id, [FromBody] ReplaceStorePayload payload, CancellationToken cancellationToken)
  {
    return Accepted(await _storeService.ReplaceAsync(id, payload, cancellationToken));
  }

  [HttpGet]
  public async Task<ActionResult<SearchResults<Store>>> SearchAsync([FromQuery] SearchStoresQuery query, CancellationToken cancellationToken)
  {
    return Ok(await _storeService.SearchAsync(query.ToPayload(), cancellationToken));
  }

  [HttpPatch("{id}")]
  public async Task<ActionResult<CommandResult>> UpdateAsync(string id, [FromBody] UpdateStorePayload payload, CancellationToken cancellationToken)
  {
    return Accepted(await _storeService.UpdateAsync(id, payload, cancellationToken));
  }
}
