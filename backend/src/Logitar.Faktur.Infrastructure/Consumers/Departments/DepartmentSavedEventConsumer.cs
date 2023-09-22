using Logitar.Faktur.Domain.Departments.Events;
using Logitar.Faktur.Infrastructure.Handlers;
using MassTransit;

namespace Logitar.Faktur.Infrastructure.Consumers.Departments;

internal class DepartmentSavedEventConsumer : IConsumer<DepartmentSavedEvent>
{
  private readonly IDepartmentEventHandler _handler;

  public DepartmentSavedEventConsumer(IDepartmentEventHandler handler)
  {
    _handler = handler;
  }

  public async Task Consume(ConsumeContext<DepartmentSavedEvent> context)
  {
    IEnumerable<TimeSpan> delays = new[] { TimeSpan.Zero }.Concat(RetryHelper.CreateDelays());
    foreach (TimeSpan delay in delays)
    {
      if (delay > TimeSpan.Zero)
      {
        Thread.Sleep(delay);
      }

      if (await _handler.HandleAsync(context.Message, context.CancellationToken))
      {
        break;
      }
    }
  }
}
