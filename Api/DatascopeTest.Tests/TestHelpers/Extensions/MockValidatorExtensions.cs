using System.Threading;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace DatascopeTest.Tests.TestHelpers.Extensions
{
    public static class MockValidatorExtensions
    {
        public static void SetupValidateAsyncFails<T>(this Mock<IValidator<T>> source, string property, string message)  
        {
            source.Setup(x => x.ValidateAsync(It.IsAny<T>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult(new[] {new ValidationFailure(property, message)}));
        }

        public static void SetupValidateAsyncPasses<T>(this Mock<IValidator<T>> source) where T : class
        {
            source.Setup(x => x.ValidateAsync(It.IsAny<T>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());
        }
    }
}