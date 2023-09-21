using Logitar.Faktur.Application;
using Logitar.Faktur.Web.Extensions;
using Logitar.Faktur.Web.Filters;
using Logitar.Faktur.Web.Settings;

namespace Logitar.Faktur.Web;

public static class DependencyInjectionExtensions
{
  public static IServiceCollection AddLogitarFakturWeb(this IServiceCollection services, IConfiguration configuration)
  {
    services
     .AddControllersWithViews(options => options.Filters.Add<ExceptionHandlingFilter>())
     .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

    CorsSettings corsSettings = configuration.GetSection("Cors").Get<CorsSettings>() ?? new();
    services.AddSingleton(corsSettings);
    services.AddCors(corsSettings);

    //services.AddAuthentication()
    //  .AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(Schemes.ApiKey, options => { })
    //  .AddScheme<BasicAuthenticationOptions, BasicAuthenticationHandler>(Schemes.Basic, options => { })
    //  .AddScheme<SessionAuthenticationOptions, SessionAuthenticationHandler>(Schemes.Session, options => { }); // TODO(fpion): Authentication

    //services.AddAuthorization(options =>
    //{
    //  options.AddPolicy(Policies.FakturActor, new AuthorizationPolicyBuilder(Schemes.All)
    //    .RequireAuthenticatedUser()
    //    .AddRequirements(new FakturActorAuthorizationRequirement())
    //    .Build());
    //}); // TODO(fpion): Authorization

    //CookiesSettings cookiesSettings = configuration.GetSection("Cookies").Get<CookiesSettings>() ?? new();
    //services.AddSingleton(cookiesSettings);
    //services.AddSession(options =>
    //{
    //  options.Cookie.SameSite = cookiesSettings.Session.SameSite;
    //  options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    //}); // TODO(fpion): Session

    services.AddDistributedMemoryCache();
    services.AddMemoryCache();
    services.AddSingleton<IApplicationContext, HttpApplicationContext>();
    //services.AddSingleton<IAuthorizationHandler, FakturActorAuthorizationHandler>(); // TODO(fpion): Authorization

    return services;
  }
}
