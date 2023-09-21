using GraphQL.Types;
using Logitar.Faktur.Contracts.Search;

namespace Logitar.Faktur.GraphQL;

internal class SearchTermGraphType : InputObjectGraphType<SearchTerm>
{
  public SearchTermGraphType()
  {
    Name = nameof(SearchTerm);
    Description = "Represents a search term.";

    Field(x => x.Value)
      .Description("The textual value of the search term.");
  }
}
