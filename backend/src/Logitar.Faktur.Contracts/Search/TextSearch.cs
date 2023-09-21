namespace Logitar.Faktur.Contracts.Search;

public record TextSearch
{
  public IEnumerable<SearchTerm> Terms { get; set; } = Enumerable.Empty<SearchTerm>();
  public SearchOperator Operator { get; set; }

  public TextSearch() : this(Enumerable.Empty<SearchTerm>())
  {
  }
  public TextSearch(IEnumerable<SearchTerm> terms, SearchOperator @operator = SearchOperator.And)
  {
    Terms = terms;
    Operator = @operator;
  }
}
