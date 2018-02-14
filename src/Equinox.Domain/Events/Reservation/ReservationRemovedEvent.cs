using System;
using Equinox.Domain.Core.Events;

namespace Equinox.Domain.Events
{
    public class ReservationRemovedEvent : Event
    {
        public ReservationRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }
    }
}