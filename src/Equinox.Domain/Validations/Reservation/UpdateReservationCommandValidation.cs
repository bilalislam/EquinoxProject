using Equinox.Domain.Commands;

namespace Equinox.Domain.Validations
{
    public class UpdateReservationCommandValidation : ReservationValidation<UpdateReservationCommand>
    {
        public UpdateReservationCommandValidation()
        {
            ValidateId();
            ValidateOwnerId();
            ValidateTitle();
            ValidateDate();
        }
    }
}