namespace Logitar.Faktur.Contracts;

public record Modification<T>
{
  public T? Value { get; set; }

  public Modification(T? value = default)
  {
    Value = value;
  }
}
