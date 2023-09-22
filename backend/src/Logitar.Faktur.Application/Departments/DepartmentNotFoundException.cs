using FluentValidation.Results;
using Logitar.Faktur.Application.Exceptions;
using Logitar.Faktur.Domain.Departments;
using Logitar.Faktur.Domain.Stores;

namespace Logitar.Faktur.Application.Departments;

public class DepartmentNotFoundException : Exception, IValidationException
{
  private const string ErrorMessage = "The specified department could not be found.";

  public StoreId StoreId
  {
    get => new((string)Data[nameof(StoreId)]!);
    private set => Data[nameof(StoreId)] = value.Value;
  }
  public DepartmentNumber Number
  {
    get => new((string)Data[nameof(Number)]!);
    private set => Data[nameof(Number)] = value.Value;
  }
  public string PropertyName
  {
    get => (string)Data[nameof(PropertyName)]!;
    private set => Data[nameof(PropertyName)] = value;
  }

  public ValidationFailure Failure => new(PropertyName, ErrorMessage, Number.Value)
  {
    CustomState = new { StoreId = StoreId.Value },
    ErrorCode = this.GetErrorCode()
  };

  public DepartmentNotFoundException(StoreId storeId, DepartmentNumber number, string propertyName)
    : base(BuildMessage(storeId, number, propertyName))
  {
    StoreId = storeId;
    Number = number;
  }

  private static string BuildMessage(StoreId storeId, DepartmentNumber number, string propertyName) => new ExceptionMessageBuilder(ErrorMessage)
    .AddData(nameof(StoreId), storeId.Value)
    .AddData(nameof(Number), number.Value)
    .AddData(nameof(PropertyName), propertyName)
    .Build();
}
