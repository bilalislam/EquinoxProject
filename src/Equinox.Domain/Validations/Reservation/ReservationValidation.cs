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

        protected void ValidateTableId()
        {
            RuleFor(c => c.TableId)
                .GreaterThanOrEqualTo(1).WithMessage("The TableId must have between 1 and 10")
                .LessThanOrEqualTo(10).WithMessage("The TableId must have between 1 and 10");
        }

        protected void ValidateDate()
        {
            RuleFor(c => c.StartDate)
                        .GreaterThanOrEqualTo(DateTime.Now).WithMessage("The StartDate must be greather or equal than today")
                        .NotEmpty().WithMessage("Please ensure you have entered the StartDate");

            RuleFor(c => c.EndDate)
            .GreaterThanOrEqualTo(DateTime.Now).WithMessage("The EndDate must be greather or equal than today")
            .NotEmpty().WithMessage("Please ensure you have entered the EndDate")
            .GreaterThan(x => x.StartDate);

            RuleFor(c => c)
            .Custom((c, context) =>
            {
                if ((c.EndDate.Subtract(c.StartDate).TotalHours != 1) || (c.StartDate.Minute != 0 || c.EndDate.Minute != 0))
                    context.AddFailure("StartDate", "the dates range diff must be hourly");

                if (!(c.StartDate.Hour >= 11 && c.StartDate.Hour <= 23 && c.EndDate.Hour >= 11 && c.EndDate.Hour <= 23))
                    context.AddFailure("StartDate", "the reservation business hourse must be beetween 11:00 and 23:00");
            });
        }
    }
}