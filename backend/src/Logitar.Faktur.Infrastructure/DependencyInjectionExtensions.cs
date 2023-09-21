using Logitar.EventSourcing.Infrastructure;
using Logitar.Faktur.Application;
using Logitar.Faktur.Application.Caching;
using Logitar.Faktur.Domain;
using Logitar.Faktur.Domain.Banners;
using Logitar.Faktur.Domain.Stores;
using Logitar.Faktur.Infrastructure.Caching;
using Logitar.Faktur.Infrastructure.Consumers.Banners;
using Logitar.Faktur.Infrastructure.Consumers.Stores;
using Logitar.Faktur.Infrastructure.Settings;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Logitar.Faktur.Infrastructure;

public static class DependencyInjectionExtensions
{
  public static IServiceCollection AddLogitarFakturInfrastructure(this IServiceCollection services)
  {
    return services
      .AddLogitarEventSourcingInfrastructure()
      .AddLogitarFakturApplication()
      .AddMassTransit(configurator =>
      {
        configurator.AddConsumers();
        configurator.UsingRabbitMq((context, configurator) =>
        {
          configurator.ConfigureJsonSerializerOptions(options =>
          {
            options.Converters.Add(new ActorIdConverter());
            options.Converters.Add(new AggregateIdConverter());
            options.Converters.Add(new DescriptionConverter());
            options.Converters.AddRange(GetJsonConverters());

            return options;
          });

          configurator.UseMessageRetry(configurator => configurator.Intervals(RetryHelper.CreateDelays().ToArray()));

          IConfiguration configuration = context.GetRequiredService<IConfiguration>();
          RabbitMqSettings settings = configuration.GetSection("RabbitMQ").Get<RabbitMqSettings>() ?? new();

          configurator.Host(settings.Host, settings.Port, settings.VirtualHost, configure =>
          {
            configure.Username(settings.Username);
            configure.Password(settings.Password);
          });
          configurator.ConfigureEndpoints(context);
        });
      })
      .AddScoped<IEventBus, EventBus>()
      .AddSingleton<ICacheService, CacheService>()
      .AddSingleton<IEventSerializer>(serviceProvider => new EventSerializer(GetJsonConverters()));
  }

  private static IBusRegistrationConfigurator AddConsumers(this IBusRegistrationConfigurator configurator)
  {
    configurator.AddConsumer<BannerCreatedEventConsumer>();
    configurator.AddConsumer<BannerDeletedEventConsumer>();
    configurator.AddConsumer<BannerUpdatedEventConsumer>();
    configurator.AddConsumer<StoreCreatedEventConsumer>();
    configurator.AddConsumer<StoreDeletedEventConsumer>();
    configurator.AddConsumer<StoreUpdatedEventConsumer>();

    return configurator;
  }

  private static IEnumerable<JsonConverter> GetJsonConverters() => new JsonConverter[]
  {
    new BannerIdConverter(),
    new DescriptionConverter(),
    new DisplayNameConverter(),
    new StoreIdConverter()
  };
}
