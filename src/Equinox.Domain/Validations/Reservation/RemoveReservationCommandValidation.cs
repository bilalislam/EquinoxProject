using Equinox.Domain.Commands;

namespace Equinox.Domain.Validations
{
    public class RemoveReservationCommandValidation : ReservationValidation<RemoveReservationCommand>
    {
        public RemoveReservationCommandValidation()
        {
            ValidateId();
        }
    }
}