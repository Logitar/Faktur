using Logitar.EventSourcing;

namespace Logitar.Faktur.Application;

public static class AggregateIdExtensions
{
  public static string Format(this AggregateId id)
  {
    try
    {
      return id.ToGuid().ToString();
    }
    catch (Exception)
    {
      return id.Value;
    }
  }
}
