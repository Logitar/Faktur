using Logitar.Faktur.Domain.Banners.Events;

namespace Logitar.Faktur.Infrastructure.Handlers;

public interface IBannerEventHandler
{
  Task HandleAsync(BannerCreatedEvent @event, CancellationToken cancellationToken = default);
  Task HandleAsync(BannerDeletedEvent @event, CancellationToken cancellationToken = default);
  Task<bool> HandleAsync(BannerUpdatedEvent @event, CancellationToken cancellationToken = default);
}
