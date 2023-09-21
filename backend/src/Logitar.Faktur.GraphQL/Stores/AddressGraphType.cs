using GraphQL.Types;
using Logitar.Faktur.Contracts.Stores;

namespace Logitar.Faktur.GraphQL.Stores;

internal class AddressGraphType : ObjectGraphType<Address>
{
  public AddressGraphType()
  {
    Name = nameof(Address);
    Description = "Represents a postal address.";

    Field(x => x.Street)
      .Description("The street address, which may include house number, street name, Post Office Box, etc., as multi-line extended information.");
    Field(x => x.Locality)
      .Description("The city or locality name.");
    Field(x => x.Region, nullable: true)
      .Description("The code of the state, province, prefecture or region of the address.");
    Field(x => x.PostalCode, nullable: true)
      .Description("The zip code or postal code of the address.");
    Field(x => x.Country)
      .Description("The code (ISO 3166-1 alpha-2) of the address's country. See https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2");
    Field(x => x.Formatted)
      .Description("The postal address formatted as the international standard for display on a mailing label.");
  }
}
