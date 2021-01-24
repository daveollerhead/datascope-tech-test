using System;
using DatascopeTest.Commands;
using DatascopeTest.Tests.TestHelpers;
using DatascopeTest.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace DatascopeTest.Tests.Validators
{
    public class UpdateGameCommandValidatorTests
    {
        private readonly UpdateGameCommandValidator _sut = new UpdateGameCommandValidator();

        [Fact]
        public void AllValuesRequired()
        {
            var model = new UpdateGameCommand();
            var result = _sut.TestValidate(model);
            result.ShouldHaveValidationErrorFor(s => s.Name);
            result.ShouldHaveValidationErrorFor(s => s.Description);
            result.ShouldHaveValidationErrorFor(s => s.ReleasedAt);
            result.ShouldHaveValidationErrorFor(s => s.Rating);
        }

        [Fact]
        public void NameAndDescriptionCannotBeGreaterThan100And500CharactersRespectively()
        {
            var model = new UpdateGameCommand
            {
                Name = Randomise.String(101),
                Description = Randomise.String(501)
            };

            var result = _sut.TestValidate(model);

            result.ShouldHaveValidationErrorFor(s => s.Name);
            result.ShouldHaveValidationErrorFor(s => s.Description);
        }

        [Fact]
        public void ReleasedAtCannotBeInTheFuture()
        {
            var model = new UpdateGameCommand
            {
                ReleasedAt = DateTime.UtcNow.AddDays(1)
            };

            var result = _sut.TestValidate(model);

            result.ShouldHaveValidationErrorFor(s => s.ReleasedAt);
        }

        [Fact]
        public void RatingCannotBeBelowAboveTen()
        {
            var model = new UpdateGameCommand
            {
                Rating = 11
            };

            var result = _sut.TestValidate(model);

            result.ShouldHaveValidationErrorFor(s => s.Rating);
        }
    }
}
