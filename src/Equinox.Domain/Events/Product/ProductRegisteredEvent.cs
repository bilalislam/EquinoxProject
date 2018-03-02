using System;
using Equinox.Domain.Core.Events;

namespace Equinox.Domain.Events
{
    public class ProductRegisteredEvent : Event
    {
        public ProductRegisteredEvent(Guid id, string name, DateTime lastUpdateDate)
        {
            Id = id;
            Name = name;
            LastUpdateDate = lastUpdateDate;
            AggregateId = id;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public DateTime LastUpdateDate { get; private set; }
    }
}