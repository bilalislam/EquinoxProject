using System;
using Equinox.Domain.Core.Events;

namespace Equinox.Domain.Events
{
    public class ReservationUpdatedEvent : Event
    {
        public ReservationUpdatedEvent(Guid id, Guid ownerId, string title, string description, DateTime startDate, DateTime endDate)
        {
            Id = id;
            OwnerId = ownerId;
            Title = title;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            AggregateId = id;
        }

        public Guid Id { get; private set; }

        public Guid OwnerId { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public DateTime StartDate { get; private set; }

        public DateTime EndDate { get; private set; }
    }
}