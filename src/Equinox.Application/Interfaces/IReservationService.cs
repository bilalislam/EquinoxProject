using System;
using System.Collections.Generic;
using Equinox.Application.EventSourcedNormalizers;
using Equinox.Application.ViewModels;

namespace Equinox.Application.Interfaces
{
    public interface IReservationService : IDisposable
    {
        void Register(ReservationViewModel model);
        IEnumerable<ReservationViewModel> GetAll();
        IEnumerable<ScheduleViewModel> GetAllByDay(DateTime day);
        IEnumerable<ScheduleViewModel> GetAvailableDays(DateTime day);
        IEnumerable<ScheduleViewModel> FindTable(int day, decimal partyOfSize, string time);
        IEnumerable<ReservationViewModel> GetReservationByDay(DateTime day);
        IEnumerable<ReservationViewModel> Check(ReservationViewModel model);
        ReservationViewModel GetById(Guid id);
        void Update(ReservationViewModel model);
        void Remove(Guid id);

    }
}
