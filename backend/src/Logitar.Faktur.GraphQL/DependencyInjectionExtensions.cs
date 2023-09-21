using GraphQL;
using GraphQL.Execution;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Logitar.Faktur.GraphQL;

public static class DependencyInjectionExtensions
{
  public static IServiceCollection AddLogitarFakturGraphQL(this IServiceCollection services, IConfiguration configuration)
  {
    GraphQLSettings settings = configuration.GetSection("GraphQL").Get<GraphQLSettings>() ?? new();

    return services.AddLogitarFakturGraphQL(settings);
  }

  public static IServiceCollection AddLogitarFakturGraphQL(this IServiceCollection services, GraphQLSettings settings)
  {
    return services.AddGraphQL(builder => builder
      .AddAuthorizationRule()
      .AddSchema<FakturSchema>()
      .AddSystemTextJson()
      .AddErrorInfoProvider(new ErrorInfoProvider(options =>
      {
        options.ExposeExceptionDetails = settings.ExposeExceptionDetails;
      }))
      .AddGraphTypes(typeof(FakturSchema).Assembly)
      .ConfigureExecutionOptions(options =>
      {
        options.EnableMetrics = settings.EnableMetrics;
      }));
  }
}
