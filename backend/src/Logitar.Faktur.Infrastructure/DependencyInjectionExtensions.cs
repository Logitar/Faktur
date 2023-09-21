using Logitar.EventSourcing.Infrastructure;
using Logitar.Faktur.Application;
using Logitar.Faktur.Domain;
using Logitar.Faktur.Infrastructure.Consumers.Banners;
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
      .AddSingleton<IEventSerializer>(serviceProvider => new EventSerializer(GetJsonConverters()));
  }

  private static IBusRegistrationConfigurator AddConsumers(this IBusRegistrationConfigurator configurator)
  {
    configurator.AddConsumer<BannerCreatedEventConsumer>();
    configurator.AddConsumer<BannerDeletedEventConsumer>();
    configurator.AddConsumer<BannerUpdatedEventConsumer>();

    return configurator;
  }

  private static IEnumerable<JsonConverter> GetJsonConverters() => new JsonConverter[]
  {
    new DescriptionConverter(),
    new DisplayNameConverter()
  };
}
