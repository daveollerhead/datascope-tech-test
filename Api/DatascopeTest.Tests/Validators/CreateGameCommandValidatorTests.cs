using System;
using DatascopeTest.Commands;
using DatascopeTest.Tests.TestHelpers;
using DatascopeTest.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace DatascopeTest.Tests.Validators
{
    public class CreateGameCommandValidatorTests
    {
        private readonly CreateGameCommandValidator _sut = new CreateGameCommandValidator();

        [Fact]
        public void AllValuesRequired()
        {
            var model = new CreateGameCommand();
            var result = _sut.TestValidate(model);
            result.ShouldHaveValidationErrorFor(s => s.Name);
            result.ShouldHaveValidationErrorFor(s => s.Description);
            result.ShouldHaveValidationErrorFor(s => s.ReleasedAt);
            result.ShouldHaveValidationErrorFor(s => s.Rating);
        }

        [Fact]
        public void NameAndDescriptionCannotBeGreaterThan100And500CharactersRespectively()
        {
            var model = new CreateGameCommand
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
            var model = new CreateGameCommand
            {
                ReleasedAt = DateTime.UtcNow.AddDays(1)
            };

            var result = _sut.TestValidate(model);

            result.ShouldHaveValidationErrorFor(s => s.ReleasedAt);
        }

        [Fact]
        public void RatingCannotBeBelowAboveTen()
        {
            var model = new CreateGameCommand
            {
                Rating = 11
            };

            var result = _sut.TestValidate(model);

            result.ShouldHaveValidationErrorFor(s => s.Rating);
        }
    }
}
