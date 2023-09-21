using Logitar.Faktur.Contracts.Stores;

namespace Logitar.Faktur.GraphQL.Stores;

internal class StoreGraphType : AggregateGraphType<Store>
{
  public StoreGraphType() : base("TODO")
  {
    Field(x => x.Number, nullable: true)
      .Description("The number of the store.");
    Field(x => x.DisplayName)
      .Description("The display name of the store.");
    Field(x => x.Description, nullable: true)
      .Description("The description of the store.");
  }
}
