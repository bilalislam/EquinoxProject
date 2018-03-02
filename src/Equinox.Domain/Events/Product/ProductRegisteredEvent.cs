using System;
using Equinox.Domain.Core.Events;

namespace Equinox.Domain.Events
{
    public class ProductRegisteredEvent : Event
    {
        public ProductRegisteredEvent(Guid id, string name)
        {
            Id = id;
            Name = name;            
            AggregateId = id;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
    }
}