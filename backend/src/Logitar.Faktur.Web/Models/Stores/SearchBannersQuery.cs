using Logitar.Faktur.Contracts.Search;
using Logitar.Faktur.Contracts.Stores;
using Microsoft.AspNetCore.Mvc;

namespace Logitar.Faktur.Web.Models.Stores;

public record SearchStoresQuery : SearchQuery
{
  [FromQuery(Name = "banner")]
  public string? BannerId { get; set; }

  public SearchStoresPayload ToPayload()
  {
    SearchStoresPayload payload = new()
    {
      IdIn = IdIn,
      BannerId = BannerId,
      Skip = Skip,
      Limit = Limit
    };
    payload.Search.Operator = SearchOperator;
    payload.Search.Terms = SearchTerms.Select(value => new SearchTerm(value));

    List<StoreSortOption> sort = new(capacity: Sort.Length);
    foreach (string sortOption in Sort)
    {
      string[] values = sortOption.Split('.');
      if (values.Length > 2)
      {
        continue;
      }

      if (!Enum.TryParse(values.Length == 2 ? values.Last() : values.Single(), out StoreSort field))
      {
        continue;
      }

      bool isDescending = values.Length == 2 && values.First().ToLower() == IsDescendingKeyword;

      sort.Add(new StoreSortOption(field, isDescending));
    }
    payload.Sort = sort;

    return payload;
  }
}
