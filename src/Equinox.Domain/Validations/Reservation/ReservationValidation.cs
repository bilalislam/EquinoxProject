using System;
using Equinox.Domain.Commands;
using FluentValidation;

namespace Equinox.Domain.Validations
{
    public abstract class ReservationValidation<T> : AbstractValidator<T> where T : ReservationCommand
    {
        protected void ValidateTitle()
        {
            RuleFor(c => c.Title)
                .NotEmpty().WithMessage("Please ensure you have entered the Title")
                .Length(6, 100).WithMessage("The Title must have between 6 and 100 characters");

            RuleFor(c => c.StartDate)
            .NotEmpty();

            RuleFor(c => c.EndDate)
            .NotEmpty()
            .GreaterThan(x => x.StartDate);

            RuleFor(c => c)
            .Custom((c, context) =>
            {
                if (c.EndDate.Subtract(c.StartDate).Hours != 1)
                    context.AddFailure("date ranges diff must be hourly");
            });
        }
    }
}