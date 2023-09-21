namespace Logitar.Faktur.Domain.Banners;

public interface IBannerRepository
{
  Task<BannerAggregate?> LoadAsync(BannerId id, CancellationToken cancellationToken = default);
  Task SaveAsync(BannerAggregate banner, CancellationToken cancellationToken = default);
}
