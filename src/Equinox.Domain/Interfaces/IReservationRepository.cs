using System;
using System.Collections.Generic;
using Equinox.Domain.Models;
using System.Linq;

namespace Equinox.Domain.Interfaces
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        IQueryable<Reservation> GetAllByRange(DateTime start, DateTime end);

        IQueryable<Reservation> GetReservationByDay(DateTime day);
    }
}