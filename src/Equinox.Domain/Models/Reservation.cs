using System;
using Equinox.Domain.Core.Models;

namespace Equinox.Domain.Models
{
    public class Reservation : Entity
    {
        public Reservation(Guid id, int taskId, int ownerId, string title, string description, DateTime startDate, DateTime endDate)
        {
            Id = id;
            TaskId = taskId;
            OwnerId = ownerId;
            Title = title;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
        }

        // Empty constructor for EF
        protected Reservation() { }

        public int TaskId { get; private set; }

        public int OwnerId { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public DateTime StartDate { get; private set; }

        public DateTime EndDate { get; private set; }
    }
}