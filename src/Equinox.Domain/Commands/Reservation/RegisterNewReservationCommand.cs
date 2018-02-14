using System;
using Equinox.Domain.Validations;

namespace Equinox.Domain.Commands
{
    public class RegisterNewReservationCommand : ReservationCommand
    {
        public RegisterNewReservationCommand(Guid ownerId, string title, string description, DateTime startDate, DateTime endDate)
        {
            OwnerId = ownerId;
            Title = title;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewReservationCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}