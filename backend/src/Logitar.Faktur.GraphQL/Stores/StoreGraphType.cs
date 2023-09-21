using Logitar.Faktur.Contracts.Stores;
using Logitar.Faktur.GraphQL.Banners;

namespace Logitar.Faktur.GraphQL.Stores;

internal class StoreGraphType : AggregateGraphType<Store>
{
  public StoreGraphType() : base("Represents a store, likely a local business that may belong in a banner, which has a physical store somewhere.")
  {
    Field(x => x.Number, nullable: true)
      .Description("The number of the store.");
    Field(x => x.DisplayName)
      .Description("The display name of the store.");
    Field(x => x.Description, nullable: true)
      .Description("The description of the store.");

    Field(x => x.Address, type: typeof(AddressGraphType))
      .Description("The postal address of the store.");
    Field(x => x.Phone, type: typeof(PhoneGraphType))
      .Description("The phone number of the store.");

    Field(x => x.Banner, type: typeof(BannerGraphType))
      .Description("The banner in which the store belongs to.");
  }
}
