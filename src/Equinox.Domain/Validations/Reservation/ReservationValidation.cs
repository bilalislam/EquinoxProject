using System;
using Equinox.Domain.Commands;
using FluentValidation;

namespace Equinox.Domain.Validations
{
    public abstract class ReservationValidation<T> : AbstractValidator<T> where T : ReservationCommand
    {
        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Please ensure you have entered the Id");
        }

        protected void ValidateOwnerId()
        {
            RuleFor(c => c.OwnerId)
            .NotEqual(Guid.Empty).WithMessage("Please ensure you have entered the OwnerId");
        }

        protected void ValidateTitle()
        {
            RuleFor(c => c.Title)
                .NotEmpty().WithMessage("Please ensure you have entered the Title")
                .Length(6, 100).WithMessage("The Title must have between 6 and 100 characters");
        }

        protected void ValidateDate()
        {
            RuleFor(c => c.StartDate)
                        .NotEmpty().WithMessage("Please ensure you have entered the StartDate");

            RuleFor(c => c.EndDate)
            .NotEmpty().WithMessage("Please ensure you have entered the EndDate")
            .GreaterThan(x => x.StartDate);

            RuleFor(c => c)
            .Custom((c, context) =>
            {
                if (c.EndDate.Subtract(c.StartDate).TotalHours != 1){
                    context.AddFailure("StartDate", "dates range diff must be hourly");
                }
            });
        }
    }
}