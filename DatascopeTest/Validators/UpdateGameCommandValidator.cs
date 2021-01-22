using DatascopeTest.Commands;
using FluentValidation;

namespace DatascopeTest.Validators
{
    public interface IUpdateGameCommandValidator : IValidator<UpdateGameCommand>
    {
    }

    public class UpdateGameCommandValidator : AbstractValidator<UpdateGameCommand>, IUpdateGameCommandValidator
    {
        public UpdateGameCommandValidator()
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
                .InclusiveBetween((byte)0, (byte)10);
        }
    }
}
