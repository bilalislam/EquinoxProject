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
    public class ScheduleService : IScheduleService
    {
        private readonly IMapper _mapper;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler Bus;

        public ScheduleService(IMapper mapper,
                                 IScheduleRepository scheduleRepository,
                                 IMediatorHandler bus,
                                 IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _scheduleRepository = scheduleRepository;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
        }

        public IEnumerable<ScheduleViewModel> GetAll()
        {
            return _scheduleRepository.GetAll().ProjectTo<ScheduleViewModel>();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
