﻿using Logitar.Faktur.Contracts.Departments;
using Logitar.Faktur.Contracts.Search;
using MediatR;

namespace Logitar.Faktur.Application.Departments.Queries;

internal class SearchDepartmentsQueryHandler : IRequestHandler<SearchDepartmentsQuery, SearchResults<Department>>
{
  private readonly IDepartmentQuerier _departmentQuerier;

  public SearchDepartmentsQueryHandler(IDepartmentQuerier departmentQuerier)
  {
    _departmentQuerier = departmentQuerier;
  }

  public async Task<SearchResults<Department>> Handle(SearchDepartmentsQuery query, CancellationToken cancellationToken)
  {
    return await _departmentQuerier.SearchAsync(query.StoreId, query.Payload, cancellationToken);
  }
}
