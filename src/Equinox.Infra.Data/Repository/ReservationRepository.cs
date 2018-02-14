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

        public IQueryable<Reservation> Check(Reservation entity)
        {
            return DbSet.AsNoTracking().Where(x => x.TableId == entity.TableId &&
            ((x.StartDate.ToShortDateString() == entity.StartDate.ToShortDateString() &&
            x.StartDate.ToShortTimeString() == entity.StartDate.ToShortTimeString() ||
            (x.EndDate.ToShortDateString() == entity.EndDate.ToShortDateString() &&
            x.EndDate.ToShortTimeString() == entity.EndDate.ToShortTimeString()))));
        }
    }
}
