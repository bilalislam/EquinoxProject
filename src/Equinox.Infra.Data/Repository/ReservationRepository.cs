using System;
using System.Collections.Generic;
using System.Linq;
using Equinox.Domain.Interfaces;
using Equinox.Domain.Models;
using Equinox.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Equinox.Infra.Data.Repository
{
    public class ReservationRepository : Repository<Reservation>, IReservationRepository
    {
        public ReservationRepository(EquinoxContext context)
            : base(context)
        {

        }

        public IQueryable<Reservation> GetReservationByDay(DateTime day)
        {
            return DbSet.AsNoTracking().Where(x => x.StartDate >= day && x.StartDate <= day.AddDays(1));
        }

        public IQueryable<Reservation> GetAllByRange(DateTime start, DateTime end)
        {
            return DbSet.AsNoTracking().Where(x =>
            ((x.StartDate.ToShortDateString() == start.ToShortDateString() &&
            x.StartDate.ToShortTimeString() == start.ToShortTimeString() ||
            (x.EndDate.ToShortDateString() == end.ToShortDateString() &&
            x.EndDate.ToShortTimeString() == end.ToShortTimeString()))));
        }
    }
}
