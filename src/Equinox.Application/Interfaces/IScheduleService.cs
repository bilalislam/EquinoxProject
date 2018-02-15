using System;
using System.Collections.Generic;
using Equinox.Application.EventSourcedNormalizers;
using Equinox.Application.ViewModels;

namespace Equinox.Application.Interfaces
{
    public interface IScheduleService : IDisposable
    {
        IEnumerable<ScheduleViewModel> GetAll();    
    }
}
