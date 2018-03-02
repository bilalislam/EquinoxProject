using System;
using Equinox.Domain.Core.Models;

namespace Equinox.Domain.Models
{
    public class Product : Entity
    {
        public Product(Guid id, string name, DateTime lastUpdateDate)
        {
            Id = id;
            Name = name;
            LastUpdateDate = lastUpdateDate;
        }

        // Empty constructor for EF
        protected Product() { }

        public string Name { get; private set; }
        public DateTime LastUpdateDate { get; set; }
    }
}