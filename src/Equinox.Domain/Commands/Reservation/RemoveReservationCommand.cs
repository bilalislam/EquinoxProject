using System;
using Equinox.Domain.Validations;

namespace Equinox.Domain.Commands
{
    public class RemoveReservationCommand : ReservationCommand
    {
        public RemoveReservationCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveReservationCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}