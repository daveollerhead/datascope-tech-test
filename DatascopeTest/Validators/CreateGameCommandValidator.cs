using DatascopeTest.Commands;
using FluentValidation;

namespace DatascopeTest.Validators
{
    public interface ICreateGameCommandValidator : IValidator<CreateGameCommand>
    {
    }

    public class CreateGameCommandValidator : AbstractValidator<CreateGameCommand>, ICreateGameCommandValidator
    {
        public CreateGameCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.ReleasedAt)
                .NotNull();

            RuleFor(x => x.Rating)
                .NotNull()
                .InclusiveBetween((byte)0, (byte)10);
        }
    }
}
