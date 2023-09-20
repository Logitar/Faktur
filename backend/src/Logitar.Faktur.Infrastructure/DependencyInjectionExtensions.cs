using Logitar.EventSourcing.Infrastructure;
using Logitar.Faktur.Application;
using Logitar.Faktur.Domain;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace Logitar.Faktur.Infrastructure;

public static class DependencyInjectionExtensions
{
  public static IServiceCollection AddLogitarFakturInfrastructure(this IServiceCollection services)
  {
    return services
      .AddLogitarEventSourcingInfrastructure()
      .AddLogitarFakturApplication()
      .AddScoped<IEventBus, EventBus>()
      .AddSingleton<IEventSerializer>(serviceProvider => new EventSerializer(new JsonConverter[]
      {
        new DescriptionConverter(),
        new DisplayNameConverter()
      }));
  }
}
