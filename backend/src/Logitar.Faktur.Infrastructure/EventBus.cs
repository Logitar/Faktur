using Logitar.EventSourcing;
using Logitar.EventSourcing.Infrastructure;
using MassTransit;
using MediatR;

namespace Logitar.Faktur.Infrastructure;

internal class EventBus : IEventBus
{
  private readonly IBus _bus;
  private readonly IPublisher _publisher;

  public EventBus(IBus bus, IPublisher publisher)
  {
    _bus = bus;
    _publisher = publisher;
  }

  public async Task PublishAsync(DomainEvent change, CancellationToken cancellationToken)
  {
    await _publisher.Publish(change, cancellationToken);

    await _bus.Publish(change, change.GetType(), cancellationToken);
  }
}
