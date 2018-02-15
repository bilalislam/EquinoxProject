using System;
using Equinox.Domain.Core.Models;

namespace Equinox.Domain.Models
{
    public class Schedule : Entity
    {
        public Schedule(string time, int tableId)
        {
            Time = time;
            TableId = tableId;
        }

        // Empty constructor for EF
        protected Schedule() { }

        public string Time { get; private set; }
        public int TableId { get; set; }
    }
}