using Equinox.Domain.Events;
using MediatR;

namespace Equinox.Domain.EventHandlers
{
    public class ReservationEventHandler :
        INotificationHandler<ReservationRegisteredEvent>,
        INotificationHandler<ReservationRemovedEvent>,
        INotificationHandler<ReservationUpdatedEvent>
    {

        public void Handle(ReservationUpdatedEvent notification)
        {

        }

        public void Handle(ReservationRemovedEvent notification)
        {

        }

        public void Handle(ReservationRegisteredEvent notification)
        {

        }
    }
}