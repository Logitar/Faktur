using Logitar.Faktur.Contracts.Banners;

namespace Logitar.Faktur.GraphQL.Banners;

internal class BannerGraphType : AggregateGraphType<Banner>
{
  public BannerGraphType() : base("Represents a group of stores operating under the same branding.")
  {
    Field(x => x.DisplayName)
      .Description("The display name of the banner.");
    Field(x => x.Description, nullable: true)
      .Description("The description of the banner.");
  }
}
