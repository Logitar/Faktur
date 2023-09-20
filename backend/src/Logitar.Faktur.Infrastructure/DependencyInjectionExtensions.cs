using Logitar.EventSourcing.Infrastructure;
using Logitar.Faktur.Application;
using Logitar.Faktur.Domain;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Logitar.Faktur.Infrastructure;

public static class DependencyInjectionExtensions
{
  public static IServiceCollection AddLogitarFakturInfrastructure(this IServiceCollection services)
  {
    //RabbitMqSettings rabbitMqSettings = configuration.GetSection("RabbitMQ").Get<RabbitMqSettings>() ?? new(); // TODO(fpion): MassTransit

    return services
      .AddLogitarEventSourcingInfrastructure()
      .AddLogitarFakturApplication()
      .AddMassTransit(configurator =>
      {
        //configurator.AddConsumers(Assembly.Load("Logitar.Faktur.EntityFrameworkCore.Relational")); // TODO(fpion): refactor
        //configurator.UsingRabbitMq((context, configurator) =>
        //{
        //  configurator.Host(rabbitMqSettings.Host, rabbitMqSettings.Port, rabbitMqSettings.VirtualHost, configure =>
        //  {
        //    configure.Username(rabbitMqSettings.Username);
        //    configure.Password(rabbitMqSettings.Password);
        //  });
        //  configurator.ConfigureEndpoints(context);
        //}); // TODO(fpion): MassTransit
      })
      .AddScoped<IEventBus, EventBus>()
      .AddSingleton<IEventSerializer>(serviceProvider => new EventSerializer(new JsonConverter[]
      {
        new DescriptionConverter(),
        new DisplayNameConverter()
      }));
  }
}
