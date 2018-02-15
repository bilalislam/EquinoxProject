using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Equinox.Application.EventSourcedNormalizers;
using Equinox.Application.Interfaces;
using Equinox.Application.ViewModels;
using Equinox.Domain.Commands;
using Equinox.Domain.Core.Bus;
using Equinox.Domain.Interfaces;
using Equinox.Infra.Data.Repository.EventSourcing;

namespace Equinox.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IMapper _mapper;
        private readonly IReservationRepository _reservationRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler Bus;
        private readonly IScheduleService _scheduleService;

        public ReservationService(IMapper mapper,
                                 IReservationRepository reservationRepository,
                                 IMediatorHandler bus,
                                 IEventStoreRepository eventStoreRepository,
                                 IScheduleService scheduleService)
        {
            _mapper = mapper;
            _reservationRepository = reservationRepository;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
            _scheduleService = scheduleService;
        }

        public IEnumerable<ReservationViewModel> GetAll()
        {
            return _reservationRepository.GetAll().ProjectTo<ReservationViewModel>();
        }


        public IEnumerable<ScheduleViewModel> GetAllByDay(DateTime day)
        {
            List<ScheduleViewModel> reservationByDayResult = GetReservationByDayAsSchedule(day).ToList();
            List<ScheduleViewModel> allTimesByDay = _scheduleService.GetAll().ToList();
            foreach (var item in allTimesByDay)
            {
                if (reservationByDayResult.Any(x => x.Time == item.Time.Substring(0, 5) && x.TableId == item.TableId))
                    item.Status = false;
            }
            return allTimesByDay;
        }

        public IEnumerable<ScheduleViewModel> GetAvailableDays(DateTime day)
        {
            IEnumerable<ScheduleViewModel> reservationByDayResult = GetReservationByDayAsSchedule(day).ToList();
            IEnumerable<ScheduleViewModel> allTimesByDay = _scheduleService.GetAll().ToList();
            List<ScheduleViewModel> result = allTimesByDay
                        .Where(w => !reservationByDayResult.Any(x => x.Time == w.Time && x.TableId == w.TableId))
                        .ToList();
            return result;
        }

        public IEnumerable<ScheduleViewModel> FindTable(int day, decimal partyOfSize, string time)
        {
            var tables = GetAvailableDays(DateTime.Today.AddDays(day)).Where(x => x.Time == time);
            decimal totalTableCount = Math.Round(partyOfSize / 2, MidpointRounding.AwayFromZero);
            var result = tables.Take((int)totalTableCount);
            if (result.Count() == totalTableCount)
                return result;
            else
                return new List<ScheduleViewModel>();
        }
        public IEnumerable<ReservationViewModel> Check(ReservationViewModel model)
        {
            var entity = _mapper.Map<Domain.Models.Reservation>(model);
            return _reservationRepository.Check(entity).ProjectTo<ReservationViewModel>();
        }

        public IEnumerable<ReservationViewModel> GetReservationByDay(DateTime day)
        {
            return _reservationRepository.GetReservationByDay(day).ProjectTo<ReservationViewModel>();
        }

        public IEnumerable<ScheduleViewModel> GetReservationByDayAsSchedule(DateTime day)
        {
            return _reservationRepository.GetReservationByDay(day).ProjectTo<ScheduleViewModel>();
        }

        public ReservationViewModel GetById(Guid id)
        {
            return _mapper.Map<ReservationViewModel>(_reservationRepository.GetById(id));
        }

        public void Register(ReservationViewModel reservationViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewReservationCommand>(reservationViewModel);
            Bus.SendCommand(registerCommand);
        }

        public void Update(ReservationViewModel reservationViewModel)
        {
            var updateCommand = _mapper.Map<UpdateReservationCommand>(reservationViewModel);
            Bus.SendCommand(updateCommand);
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemoveReservationCommand(id);
            Bus.SendCommand(removeCommand);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}
