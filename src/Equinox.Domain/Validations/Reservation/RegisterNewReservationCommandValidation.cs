using Equinox.Domain.Commands;

namespace Equinox.Domain.Validations
{
    public class RegisterNewReservationCommandValidation : ReservationValidation<RegisterNewReservationCommand>
    {
        public RegisterNewReservationCommandValidation()
        {
            ValidateOwnerId();
            ValidateTitle();
            ValidateDate();
        }
    }
}