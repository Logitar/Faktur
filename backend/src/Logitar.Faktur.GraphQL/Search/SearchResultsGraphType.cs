using GraphQL.Types;
using Logitar.Faktur.Contracts.Search;

namespace Logitar.Faktur.GraphQL;

internal abstract class SearchResultsGraphType<TGraphType, TSourceType> : ObjectGraphType<SearchResults<TSourceType>>
  where TGraphType : ObjectGraphType<TSourceType>
{
  public SearchResultsGraphType(string name, string? description = null)
  {
    Name = name;
    Description = description ?? "Represents the results of a search.";

    Field(x => x.Results, type: typeof(NonNullGraphType<ListGraphType<NonNullGraphType<TGraphType>>>))
      .Description("The list of matching results.");
    Field(x => x.Total)
      .Description("The total number of matching results.");
  }
}
