﻿using System;
using DatascopeTest.Commands;
using FluentValidation;

namespace DatascopeTest.Validators
{
    public class CreateGameCommandValidator : AbstractValidator<CreateGameCommand>, IValidator<CreateGameCommand> 
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
                .NotNull()
                .LessThanOrEqualTo(DateTime.UtcNow);

            RuleFor(x => x.Rating)
                .NotNull()
                .InclusiveBetween((byte)0, (byte)10);
        }
    }
}
