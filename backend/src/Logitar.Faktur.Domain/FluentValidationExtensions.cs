﻿using FluentValidation;

namespace Logitar.Faktur.Domain;

internal static class FluentValidationExtensions
{
  public static IRuleBuilderOptions<T, TProperty> WithPropertyName<T, TProperty>(this IRuleBuilderOptions<T, TProperty> options, string? propertyName)
  {
    return propertyName == null ? options : options.OverridePropertyName(propertyName).WithName(propertyName);
  }
}
