﻿using Logitar.EventSourcing.EntityFrameworkCore.Relational;
using Logitar.Faktur.Application.Actors;
using Logitar.Faktur.Application.Banners;
using Logitar.Faktur.Domain.Banners;
using Logitar.Faktur.EntityFrameworkCore.Relational.Actors;
using Logitar.Faktur.EntityFrameworkCore.Relational.Handlers;
using Logitar.Faktur.EntityFrameworkCore.Relational.Queriers;
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
      .AddQueriers()
      .AddRepositories()
      .AddScoped<IActorService, ActorService>();
  }

  private static IServiceCollection AddEventHandlers(this IServiceCollection services)
  {
    return services.AddScoped<IBannerEventHandler, BannerEventHandler>();
  }

  private static IServiceCollection AddQueriers(this IServiceCollection services)
  {
    return services.AddScoped<IBannerQuerier, BannerQuerier>();
  }

  private static IServiceCollection AddRepositories(this IServiceCollection services)
  {
    return services.AddScoped<IBannerRepository, BannerRepository>();
  }
}
