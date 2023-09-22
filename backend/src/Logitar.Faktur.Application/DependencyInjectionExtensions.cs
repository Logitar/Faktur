using Logitar.Faktur.Application.Banners;
using Logitar.Faktur.Application.Departments;
using Logitar.Faktur.Application.Stores;
using Logitar.Faktur.Contracts.Banners;
using Logitar.Faktur.Contracts.Departments;
using Logitar.Faktur.Contracts.Stores;
using Microsoft.Extensions.DependencyInjection;

namespace Logitar.Faktur.Application;

public static class DependencyInjectionExtensions
{
  public static IServiceCollection AddLogitarFakturApplication(this IServiceCollection services)
  {
    Assembly assembly = typeof(DependencyInjectionExtensions).Assembly;

    return services
      .AddApplicationServices()
      .AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
  }

  private static IServiceCollection AddApplicationServices(this IServiceCollection services)
  {
    return services
      .AddTransient<IBannerService, BannerService>()
      .AddTransient<IDepartmentService, DepartmentService>()
      .AddTransient<IStoreService, StoreService>();
  }
}
