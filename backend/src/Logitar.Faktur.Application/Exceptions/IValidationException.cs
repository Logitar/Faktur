using FluentValidation.Results;

namespace Logitar.Faktur.Application.Exceptions;

public interface IValidationException
{
  ValidationFailure Failure { get; }
}
