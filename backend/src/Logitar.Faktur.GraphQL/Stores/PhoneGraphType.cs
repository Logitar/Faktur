using GraphQL.Types;
using Logitar.Faktur.Contracts.Stores;

namespace Logitar.Faktur.GraphQL.Stores;

internal class PhoneGraphType : ObjectGraphType<Phone>
{
  public PhoneGraphType()
  {
    Name = nameof(Phone);
    Description = "Represents a phone number.";

    Field(x => x.CountryCode, nullable: true)
      .Description("The country code. See https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2");
    Field(x => x.Number)
      .Description("The phone number.");
    Field(x => x.Extension, nullable: true)
      .Description("The phone extension.");
    Field(x => x.E164Formatted)
      .Description("The phone formatted to the E.164 format. See https://en.wikipedia.org/wiki/E.164");
  }
}
