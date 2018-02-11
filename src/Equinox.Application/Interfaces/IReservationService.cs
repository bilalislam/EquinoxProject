using System;
using System.Collections.Generic;
using Equinox.Application.EventSourcedNormalizers;
using Equinox.Application.ViewModels;

namespace Equinox.Application.Interfaces
{
    public interface IReservationService : IDisposable
    {
        void Register(ReservationViewModel customerViewModel);
        IEnumerable<ReservationViewModel> GetAll();
        IEnumerable<ReservationViewModel> GetAllByRange(DateTime start, DateTime end);
        ReservationViewModel GetById(Guid id);
        void Update(ReservationViewModel customerViewModel);
        void Remove(Guid id);

    }
}
