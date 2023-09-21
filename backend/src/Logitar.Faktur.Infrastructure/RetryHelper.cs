namespace Logitar.Faktur.Infrastructure;

internal static class RetryHelper
{
  public const int MaximumCount = 7;

  public static IEnumerable<TimeSpan> CreateDelays() => Enumerable.Range(0, MaximumCount - 1)
    .Select(i => TimeSpan.FromSeconds(Math.Pow(2, i)));
}
