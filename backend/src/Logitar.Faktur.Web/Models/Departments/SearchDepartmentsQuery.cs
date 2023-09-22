using Logitar.Faktur.Contracts.Departments;
using Logitar.Faktur.Contracts.Search;

namespace Logitar.Faktur.Web.Models.Departments;

public record SearchDepartmentsQuery : SearchQuery
{
  public SearchDepartmentsPayload ToPayload()
  {
    SearchDepartmentsPayload payload = new()
    {
      IdIn = IdIn,
      Skip = Skip,
      Limit = Limit
    };
    payload.Search.Operator = SearchOperator;
    payload.Search.Terms = SearchTerms.Select(value => new SearchTerm(value));

    List<DepartmentSortOption> sort = new(capacity: Sort.Length);
    foreach (string sortOption in Sort)
    {
      string[] values = sortOption.Split('.');
      if (values.Length > 2)
      {
        continue;
      }

      if (!Enum.TryParse(values.Length == 2 ? values.Last() : values.Single(), out DepartmentSort field))
      {
        continue;
      }

      bool isDescending = values.Length == 2 && values.First().ToLower() == IsDescendingKeyword;

      sort.Add(new DepartmentSortOption(field, isDescending));
    }
    payload.Sort = sort;

    return payload;
  }
}
