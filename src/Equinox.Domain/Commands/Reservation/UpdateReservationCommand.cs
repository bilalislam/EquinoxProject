using System;
using Equinox.Domain.Validations;

namespace Equinox.Domain.Commands
{
    public class UpdateReservationCommand : ReservationCommand
    {
        public UpdateReservationCommand(Guid id, Guid ownerId, string title, string description, DateTime startDate, DateTime endDate, int tableId)
        {
            Id = id;
            OwnerId = ownerId;
            Title = title;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            TableId = tableId;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateReservationCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}