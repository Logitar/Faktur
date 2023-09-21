using Logitar.Faktur.Contracts.Banners;

namespace Logitar.Faktur.Application.Banners;

public interface IBannerQuerier
{
  Task<Banner?> ReadAsync(string id, CancellationToken cancellationToken = default);
}
