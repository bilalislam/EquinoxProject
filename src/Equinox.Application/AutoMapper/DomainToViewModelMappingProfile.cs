using AutoMapper;
using Equinox.Application.ViewModels;
using Equinox.Domain.Models;

namespace Equinox.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Customer, CustomerViewModel>();
            CreateMap<Reservation, ReservationViewModel>();
            CreateMap<Reservation, ScheduleViewModel>()
                .ForMember(x => x.TableId, opt => opt.MapFrom(x => x.TableId))
                .ForMember(x => x.Time, opt => opt.MapFrom(x => x.StartDate.ToShortTimeString()));
            CreateMap<ReservationViewModel, Reservation>();
            CreateMap<Schedule, ScheduleViewModel>()
            .ForMember(x => x.TableId, opt => opt.MapFrom(x => x.TableId))
            .ForMember(x => x.Time, opt => opt.MapFrom(x => x.Time.Substring(0, 5)));
        }
    }
}
