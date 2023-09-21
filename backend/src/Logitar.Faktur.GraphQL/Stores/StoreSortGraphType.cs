using GraphQL.Types;
using Logitar.Faktur.Contracts.Stores;

namespace Logitar.Faktur.GraphQL.Stores;

internal class StoreSortGraphType : EnumerationGraphType<StoreSort>
{
  public StoreSortGraphType()
  {
    Name = nameof(StoreSort);
    Description = "Represents the available store fields for sorting.";

    Add(StoreSort.DisplayName, "The stores will be sorted by their display name.");
    Add(StoreSort.Number, "The stores will be sorted by their number.");
    Add(StoreSort.UpdatedOn, "The stores will be sorted by their latest update date and time.");
  }

  private void Add(StoreSort value, string description) => Add(value.ToString(), value, description);
}
