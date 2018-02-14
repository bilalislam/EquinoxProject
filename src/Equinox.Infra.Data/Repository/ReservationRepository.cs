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

        public IQueryable<Reservation> GetAllByRange(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
