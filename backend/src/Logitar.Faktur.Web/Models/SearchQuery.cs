using Logitar.Faktur.Contracts.Search;
using Microsoft.AspNetCore.Mvc;

namespace Logitar.Faktur.Web.Models;

public record SearchQuery
{
  protected const string IsDescendingKeyword = "desc";

  [FromQuery(Name = "id_terms")]
  public string[] IdTerms { get; set; } = Array.Empty<string>();
  [FromQuery(Name = "id_operator")]
  public SearchOperator IdOperator { get; set; }

  [FromQuery(Name = "search_terms")]
  public string[] SearchTerms { get; set; } = Array.Empty<string>();
  [FromQuery(Name = "search_operator")]
  public SearchOperator SearchOperator { get; set; }

  [FromQuery(Name = "sort")]
  public string[] Sort { get; set; } = Array.Empty<string>();

  [FromQuery(Name = "skip")]
  public int Skip { get; set; }

  [FromQuery(Name = "limit")]
  public int Limit { get; set; }
}
