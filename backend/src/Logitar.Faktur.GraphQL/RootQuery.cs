using GraphQL.Types;
using Logitar.Faktur.GraphQL.Banners;
using Logitar.Faktur.GraphQL.Stores;

namespace Logitar.Faktur.GraphQL;

internal class RootQuery : ObjectGraphType
{
  public RootQuery()
  {
    Name = nameof(RootQuery);

    BannerQueries.Register(this);
    StoreQueries.Register(this);
  }
}
