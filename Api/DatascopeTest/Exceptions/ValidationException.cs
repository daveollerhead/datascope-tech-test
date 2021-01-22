using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace DatascopeTest.Exceptions
{
    public class ValidationException : Exception
    {
        public IEnumerable<ValidationFailure> Errors { get; }

        public ValidationException(IEnumerable<ValidationFailure> errors)
        {
            Errors = errors;
        }
    }
}