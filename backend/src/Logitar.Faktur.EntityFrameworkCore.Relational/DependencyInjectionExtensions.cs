using Logitar.EventSourcing.EntityFrameworkCore.Relational;
using Logitar.Faktur.Domain.Banners;
using Logitar.Faktur.EntityFrameworkCore.Relational.Handlers;
using Logitar.Faktur.EntityFrameworkCore.Relational.Repositories;
using Logitar.Faktur.Infrastructure;
using Logitar.Faktur.Infrastructure.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace Logitar.Faktur.EntityFrameworkCore.Relational;

public static class DependencyInjectionExtensions
{
  public static IServiceCollection AddLogitarFakturWithEntityFrameworkCoreRelational(this IServiceCollection services)
  {
    Assembly assembly = typeof(DependencyInjectionExtensions).Assembly;

    return services
      .AddEventHandlers()
      .AddLogitarEventSourcingWithEntityFrameworkCoreRelational()
      .AddLogitarFakturInfrastructure()
      .AddMediatR(config => config.RegisterServicesFromAssembly(assembly))
      .AddRepositories();
  }

  private static IServiceCollection AddEventHandlers(this IServiceCollection services)
  {
    return services.AddScoped<IBannerEventHandler, BannerEventHandler>();
  }

  private static IServiceCollection AddRepositories(this IServiceCollection services)
  {
    return services.AddScoped<IBannerRepository, BannerRepository>();
  }
}
