using GraphQL.Types;
using Logitar.Faktur.GraphQL.Banners;

namespace Logitar.Faktur.GraphQL;

internal class RootQuery : ObjectGraphType
{
  public RootQuery()
  {
    Name = nameof(RootQuery);

    BannerQueries.Register(this);
  }
}
