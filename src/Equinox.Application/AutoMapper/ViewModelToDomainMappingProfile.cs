using AutoMapper;
using Equinox.Application.ViewModels;
using Equinox.Domain.Commands;

namespace Equinox.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<CustomerViewModel, RegisterNewCustomerCommand>()
                .ConstructUsing(c => new RegisterNewCustomerCommand(c.Name, c.Email, c.BirthDate));

            CreateMap<CustomerViewModel, UpdateCustomerCommand>()
                .ConstructUsing(c => new UpdateCustomerCommand(c.Id, c.Name, c.Email, c.BirthDate));

            CreateMap<ReservationViewModel, RegisterNewReservationCommand>()
            .ConstructUsing(c => new RegisterNewReservationCommand(c.OwnerId, c.Title, c.Description, c.StartDate, c.EndDate));

            CreateMap<ReservationViewModel, UpdateReservationCommand>()
                .ConstructUsing(c => new UpdateReservationCommand(c.Id, c.OwnerId, c.Title, c.Description, c.StartDate, c.EndDate));
        }
    }
}
