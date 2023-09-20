using Logitar.Faktur.Contracts.Banners;
using Logitar.Faktur.Contracts.Search;

namespace Logitar.Faktur.Web.Models.Banners;

public record SearchBannersQuery : SearchQuery
{
  public SearchBannersPayload ToPayload()
  {
    SearchBannersPayload payload = new()
    {
      IdIn = IdIn,
      Skip = Skip,
      Limit = Limit
    };
    payload.Search.Operator = SearchOperator;
    payload.Search.Terms = SearchTerms.Select(value => new SearchTerm(value));

    List<BannerSortOption> sort = new(capacity: Sort.Length);
    foreach (string sortOption in Sort)
    {
      string[] values = sortOption.Split('.');
      if (values.Length > 2)
      {
        continue;
      }

      if (!Enum.TryParse(values.Length == 2 ? values.Last() : values.Single(), out BannerSort field))
      {
        continue;
      }

      bool isDescending = values.Length == 2 && values.First().ToLower() == IsDescendingKeyword;

      sort.Add(new BannerSortOption(field, isDescending));
    }
    payload.Sort = sort;

    return payload;
  }
}
