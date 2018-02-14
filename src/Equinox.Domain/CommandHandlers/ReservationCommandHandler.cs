using System;
using Equinox.Domain.Commands;
using Equinox.Domain.Core.Bus;
using Equinox.Domain.Core.Notifications;
using Equinox.Domain.Events;
using Equinox.Domain.Interfaces;
using Equinox.Domain.Models;
using MediatR;


namespace Equinox.Domain.CommandHandlers
{
    public class ReservationCommandHandler : CommandHandler,
        INotificationHandler<RegisterNewReservationCommand>,
        INotificationHandler<UpdateReservationCommand>,
        INotificationHandler<RemoveReservationCommand>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMediatorHandler Bus;

        public ReservationCommandHandler(IReservationRepository reservationRepository,
                                      IUnitOfWork uow,
                                      IMediatorHandler bus,
                                      INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _reservationRepository = reservationRepository;
            Bus = bus;
        }

        public void Handle(RegisterNewReservationCommand message)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return;
            }
        }

        public void Handle(UpdateReservationCommand message)
        {

        }

        public void Handle(RemoveReservationCommand message)
        {

        }

        public void Dispose()
        {
            _reservationRepository.Dispose();
        }
    }
}