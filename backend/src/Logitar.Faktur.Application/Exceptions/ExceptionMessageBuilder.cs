namespace Logitar.Faktur.Application.Exceptions;

internal class ExceptionMessageBuilder
{
  private readonly StringBuilder _message = new();

  public ExceptionMessageBuilder(string? message = null)
  {
    if (message != null)
    {
      _message.AppendLine(message);
    }
  }

  public ExceptionMessageBuilder AddData(string key, object? value)
  {
    _message.Append(key).Append(": ").Append(value).AppendLine();
    return this;
  }

  public string Build() => _message.ToString();
}
