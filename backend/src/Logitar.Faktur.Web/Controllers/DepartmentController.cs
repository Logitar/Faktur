using Logitar.Faktur.Contracts.Departments;
using Logitar.Faktur.Contracts.Search;
using Logitar.Faktur.Web.Models.Departments;
using Microsoft.AspNetCore.Mvc;

namespace Logitar.Faktur.Web.Controllers;

[ApiController]
[Route("departments/{storeId}/departments")]
public class DepartmentController : ControllerBase // TODO(fpion): Authorization
{
  private readonly IDepartmentService _departmentService;

  public DepartmentController(IDepartmentService departmentService)
  {
    _departmentService = departmentService;
  }

  [HttpDelete("{number}")]
  public async Task<ActionResult<Department>> DeleteAsync(string storeId, string number, CancellationToken cancellationToken)
  {
    return Ok(await _departmentService.RemoveAsync(storeId, number, cancellationToken));
  }

  [HttpGet("{number}")]
  public async Task<ActionResult<Department>> ReadAsync(string storeId, string number, CancellationToken cancellationToken)
  {
    Department? department = await _departmentService.ReadAsync(storeId, number, cancellationToken);
    return department == null ? NotFound() : Ok(department);
  }

  [HttpPut("{number}")]
  public async Task<ActionResult<Department>> SaveAsync(string storeId, string number, SaveDepartmentPayload payload, CancellationToken cancellationToken)
  {
    return Ok(await _departmentService.SaveAsync(storeId, number, payload, cancellationToken));
  }

  [HttpGet]
  public async Task<ActionResult<SearchResults<Department>>> SearchAsync(string id, SearchDepartmentsQuery query, CancellationToken cancellationToken)
  {
    return Ok(await _departmentService.SearchAsync(id, query.ToPayload(), cancellationToken));
  }

  [HttpPatch("{number}")]
  public async Task<ActionResult<Department>> UpdateAsync(string storeId, string number, UpdateDepartmentPayload payload, CancellationToken cancellationToken)
  {
    return Ok(await _departmentService.UpdateAsync(storeId, number, payload, cancellationToken));
  }
}
