using System;
using System.Collections.Generic;
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

         public ReservationService(IMapper mapper,
                                  IReservationRepository reservationRepository,
                                  IMediatorHandler bus,
                                  IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _reservationRepository = reservationRepository;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
        }

        public IEnumerable<ReservationViewModel> GetAll()
        {
            return _reservationRepository.GetAll().ProjectTo<ReservationViewModel>();
        }

        public IEnumerable<ReservationViewModel> GetAllByRange(DateTime start, DateTime end)
        {
            return _reservationRepository.GetAllByRange(start, end).ProjectTo<ReservationViewModel>();
        }

        public IEnumerable<ReservationViewModel> GetReservationByDay(DateTime day)
        {
            return _reservationRepository.GetReservationByDay(day).ProjectTo<ReservationViewModel>();
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
