
using System;
using Equinox.Domain.Core.Commands;

namespace Equinox.Domain.Commands
{
    public abstract class ReservationCommand : Command
    {
        public Guid Id { get; protected set; }

        public Guid OwnerId { get; protected set; }

        public string Title { get; protected set; }

        public string Description { get; protected set; }

        public DateTime StartDate { get; protected set; }

        public DateTime EndDate { get; protected set; }

        public int TableId { get; set; }
    }
}