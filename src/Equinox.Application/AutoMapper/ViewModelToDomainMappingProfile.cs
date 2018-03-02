using AutoMapper;
using Equinox.Application.ViewModels;
using Equinox.Domain.Commands;

namespace Equinox.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {

            CreateMap<ProductViewModel, RegisterNewProductCommand>()
                .ConstructUsing(c => new RegisterNewProductCommand(c.Name));

            CreateMap<ProductViewModel, UpdateProductCommand>()
                .ConstructUsing(c => new UpdateProductCommand(c.Id, c.Name, c.LastUpdateDate.Value));
        }
    }
}
