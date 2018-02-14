using System;
using System.Linq;
using System.Collections.Generic;
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

            var reservation = new Reservation(Guid.NewGuid(), message.OwnerId, message.Title, message.Description, message.StartDate, message.EndDate);

            if (_reservationRepository.GetAllByRange(reservation.StartDate, reservation.EndDate).Count() > 0)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "The reservation has already been taken at this date range."));
                return;
            }

            _reservationRepository.Add(reservation);

            if (Commit())
            {
                Bus.RaiseEvent(new ReservationRegisteredEvent(reservation.Id, reservation.OwnerId, reservation.Title, reservation.Description, reservation.StartDate, reservation.EndDate));
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