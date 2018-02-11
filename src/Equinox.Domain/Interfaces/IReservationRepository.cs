using System;
using System.Collections.Generic;
using Equinox.Domain.Models;

namespace Equinox.Domain.Interfaces
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        IEnumerable<Reservation> GetAllByRange(DateTime start, DateTime end);
    }
}