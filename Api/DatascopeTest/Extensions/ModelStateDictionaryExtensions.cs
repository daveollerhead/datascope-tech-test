using System.Collections.Generic;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DatascopeTest.Extensions
{
    public static class ModelStateDictionaryExtensions
    {
        public static void AddModelErrorRange(this ModelStateDictionary source, IEnumerable<ValidationFailure> errors)
        {
            foreach (var error in errors)
            {
                source.AddModelError(error.PropertyName, error.ErrorMessage);
            }
        }
    }
}