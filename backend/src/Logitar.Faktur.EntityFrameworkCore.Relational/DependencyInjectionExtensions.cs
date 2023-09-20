using Logitar.EventSourcing.EntityFrameworkCore.Relational;
using Logitar.Faktur.Domain.Banners;
using Logitar.Faktur.EntityFrameworkCore.Relational.Repositories;
using Logitar.Faktur.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Logitar.Faktur.EntityFrameworkCore.Relational;

public static class DependencyInjectionExtensions
{
  public static IServiceCollection AddLogitarFakturWithEntityFrameworkCoreRelational(this IServiceCollection services)
  {
    Assembly assembly = typeof(DependencyInjectionExtensions).Assembly;

    return services
      .AddLogitarEventSourcingWithEntityFrameworkCoreRelational()
      .AddLogitarFakturInfrastructure()
      .AddMediatR(config => config.RegisterServicesFromAssembly(assembly))
      .AddRepositories();
  }

  private static IServiceCollection AddRepositories(this IServiceCollection services)
  {
    return services.AddScoped<IBannerRepository, BannerRepository>();
  }
}
