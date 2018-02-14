using System;
using System.Collections.Generic;
using Equinox.Domain.Models;
using System.Linq;

namespace Equinox.Domain.Interfaces
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        IQueryable<Reservation> Check(Reservation entity);

        IQueryable<Reservation> GetReservationByDay(DateTime day);
    }
}