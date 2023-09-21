using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Logitar.Faktur.GraphQL;

public class FakturSchema : Schema
{
  public FakturSchema(IServiceProvider serviceProvider) : base(serviceProvider)
  {
    Query = serviceProvider.GetRequiredService<RootQuery>();
  }
}
