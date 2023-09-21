using Logitar.Faktur.Contracts.Search;

namespace Logitar.Faktur.Contracts.Banners;

public interface IBannerService
{
  Task<CommandResult> CreateAsync(CreateBannerPayload payload, CancellationToken cancellationToken = default);
  Task<CommandResult> DeleteAsync(string id, CancellationToken cancellationToken = default);
  Task<Banner?> ReadAsync(string id, CancellationToken cancellationToken = default);
  Task<CommandResult> ReplaceAsync(string id, ReplaceBannerPayload payload, CancellationToken cancellationToken = default);
  Task<SearchResults<Banner>> SearchAsync(SearchBannersPayload payload, CancellationToken cancellationToken = default);
  Task<CommandResult> UpdateAsync(string id, UpdateBannerPayload payload, CancellationToken cancellationToken = default);
}
