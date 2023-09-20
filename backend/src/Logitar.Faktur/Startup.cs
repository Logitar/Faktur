using Logitar.EventSourcing.EntityFrameworkCore.Relational;
using Logitar.Faktur.EntityFrameworkCore.Relational;
using Logitar.Faktur.EntityFrameworkCore.SqlServer;
using Logitar.Faktur.Web;
using Logitar.Faktur.Web.Extensions;

namespace Logitar.Faktur;

internal class Startup : StartupBase
{
  private readonly IConfiguration _configuration;
  private readonly bool _enableOpenApi;

  public Startup(IConfiguration configuration)
  {
    _configuration = configuration;
    _enableOpenApi = configuration.GetValue<bool>("EnableOpenApi");
  }

  public override void ConfigureServices(IServiceCollection services)
  {
    base.ConfigureServices(services);

    services.AddLogitarFakturWeb(_configuration);
    //services.AddLogitarFakturGraphQL(_configuration); // TODO(fpion): GraphQL

    services.AddApplicationInsightsTelemetry();
    IHealthChecksBuilder healthChecks = services.AddHealthChecks();

    if (_enableOpenApi)
    {
      services.AddOpenApi();
    }

    DatabaseProvider databaseProvider = _configuration.GetValue<DatabaseProvider?>("DatabaseProvider")
      ?? DatabaseProvider.EntityFrameworkCorePostgreSQL;
    switch (databaseProvider)
    {
      //case DatabaseProvider.EntityFrameworkCorePostgreSQL:
      //  connectionString = _configuration.GetValue<string>("POSTGRESQLCONNSTR_Faktur") ?? string.Empty;
      //  healthChecks.AddDbContextCheck<EventContext>();
      //  healthChecks.AddDbContextCheck<FakturContext>();
      //  break; // TODO(fpion): PostgreSQL
      case DatabaseProvider.EntityFrameworkCoreSqlServer:
        services.AddLogitarFakturWithEntityFrameworkCoreSqlServer(_configuration);
        healthChecks.AddDbContextCheck<EventContext>();
        healthChecks.AddDbContextCheck<FakturContext>();
        break;
      default:
        throw new DatabaseProviderNotSupportedException(databaseProvider);
    }
  }

  public override void Configure(IApplicationBuilder builder)
  {
    if (_enableOpenApi)
    {
      builder.UseOpenApi();
    }

    //if (_configuration.GetValue<bool>("UseGraphQLAltair"))
    //{
    //  builder.UseGraphQLAltair();
    //}
    //if (_configuration.GetValue<bool>("UseGraphQLGraphiQL"))
    //{
    //  builder.UseGraphQLGraphiQL();
    //}
    //if (_configuration.GetValue<bool>("UseGraphQLPlayground"))
    //{
    //  builder.UseGraphQLPlayground();
    //}
    //if (_configuration.GetValue<bool>("UseGraphQLVoyager"))
    //{
    //  builder.UseGraphQLVoyager();
    //} // TODO(fpion): GraphQL

    builder.UseHttpsRedirection();
    builder.UseCors();
    //builder.UseSession(); // TODO(fpion): Session
    //builder.UseMiddleware<RenewSession>(); // TODO(fpion): Session
    //builder.UseAuthentication(); // TODO(fpion): Authentication
    //builder.UseAuthorization(); // TODO(fpion): Authorization

    //builder.UseGraphQL<FakturSchema>("/graphql", options => options.AuthenticationSchemes.AddRange(Schemes.All)); // TODO(fpion): GraphQL

    if (builder is WebApplication application)
    {
      application.MapControllers();
      application.MapHealthChecks("/health");
    }
  }
}
