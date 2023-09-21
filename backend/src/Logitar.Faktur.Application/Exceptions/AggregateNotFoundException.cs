using FluentValidation.Results;
using Logitar.EventSourcing;

namespace Logitar.Faktur.Application.Exceptions;

public class AggregateNotFoundException : Exception, IValidationException
{
  private const string ErrorMessage = "The specified aggregate could not be found.";

  public Type Type
  {
    get
    {
      string typeName = (string)Data[nameof(Type)]!;
      return Type.GetType(typeName) ?? throw new InvalidOperationException($"The type '{typeName}' could not be resolved.");
    }
    private set => Data[nameof(Type)] = value.GetName();
  }
  public string Id
  {
    get => (string)Data[nameof(Id)]!;
    private set => Data[nameof(Id)] = value;
  }
  public string PropertyName
  {
    get => (string)Data[nameof(PropertyName)]!;
    private set => Data[nameof(PropertyName)] = value;
  }

  public ValidationFailure Failure => new(PropertyName, ErrorMessage, Id)
  {
    ErrorCode = this.GetErrorCode()
  };

  public AggregateNotFoundException(Type type, string id, string propertyName)
    : base(BuildMessage(type, id, propertyName))
  {
    if (!type.IsSubclassOf(typeof(AggregateRoot)))
    {
      throw new ArgumentException($"The type must be a subclass of {nameof(AggregateRoot)}.", nameof(type));
    }

    Type = type;
    Id = id;
    PropertyName = propertyName;
  }

  private static string BuildMessage(Type type, string id, string propertyName) => new ExceptionMessageBuilder(ErrorMessage)
    .AddData(nameof(Type), type.GetName())
    .AddData(nameof(Id), id)
    .AddData(nameof(PropertyName), propertyName)
    .Build();
}

public class AggregateNotFoundException<T> : AggregateNotFoundException where T : AggregateRoot
{
  public AggregateNotFoundException(AggregateId id, string propertyName)
    : base(typeof(T), id.Value, propertyName)
  {
  }
}
