using Logitar.EventSourcing;
using Logitar.Faktur.Application.Caching;
using Logitar.Faktur.Contracts.Actors;
using Microsoft.Extensions.Caching.Memory;

namespace Logitar.Faktur.Infrastructure.Caching;

internal class CacheService : ICacheService
{
  private readonly IMemoryCache _cache;

  public CacheService(IMemoryCache cache)
  {
    _cache = cache;
  }

  public Actor? GetActor(ActorId id) => GetItem<Actor>(GetActorKey(id));
  public void RemoveActor(ActorId id) => RemoveItem(GetActorKey(id));
  public void SetActor(Actor actor) => SetItem(GetActorKey(actor.Id), actor, TimeSpan.FromHours(1));
  private static string GetActorKey(string id) => GetActorKey(new ActorId(id));
  private static string GetActorKey(ActorId id) => $"Actor:{id}";

  private T? GetItem<T>(string key) => _cache.TryGetValue(key, out object? value) ? (T?)value : default;
  private void RemoveItem(string key) => _cache.Remove(key);
  private void SetItem<T>(string key, T value, TimeSpan duration) => _cache.Set(key, value, duration);
}
