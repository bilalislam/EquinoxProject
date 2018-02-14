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
using Equinox.Domain.Core.Commands;

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
            Validate(message);
            var reservation = new Reservation(Guid.NewGuid(), message.OwnerId, message.Title, 
                    message.Description, message.StartDate, message.EndDate, message.TableId);
            Check(message, reservation);
            _reservationRepository.Add(reservation);
            if (Commit())
                RaiseEvent(new ReservationRegisteredEvent(reservation.Id, reservation.OwnerId, reservation.Title, 
                reservation.Description, reservation.StartDate, reservation.EndDate, message.TableId));
        }

        public void Handle(UpdateReservationCommand message)
        {
            Validate(message);
            var reservation = new Reservation(message.Id, message.OwnerId, message.Title,
             message.Description, message.StartDate, message.EndDate, message.TableId);
            Check(message, reservation);
            _reservationRepository.Update(reservation);
            if (Commit())
                RaiseEvent(new ReservationUpdatedEvent(reservation.Id, reservation.OwnerId, reservation.Title,
                 reservation.Description, reservation.StartDate, reservation.EndDate, message.TableId));
        }

        public void Handle(RemoveReservationCommand message)
        {
            Validate(message);
            _reservationRepository.Remove(message.Id);
            if (Commit())
                Bus.RaiseEvent(new ReservationRemovedEvent(message.Id));
        }

        public void Dispose()
        {
            _reservationRepository.Dispose();
        }


        /*
            * müsaitlik durumu hem tarih hem de masaya göre kontrol edilmeli.
         */
        private void Check(Command message, Reservation reservation)
        {
            var result = _reservationRepository.Check(reservation);
            if (result.Any(x => x.Id != reservation.Id))
                RaiseError(message, "The reservation has already been taken at this date range.");
        }
    }
}