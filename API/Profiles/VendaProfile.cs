using API.Dtos;
using AutoMapper;
using Domain.Entities;

namespace API.Profiles
{
    public class VendaProfile : Profile
    {
        public VendaProfile()
        {
            CreateMap<Venda, RegistrarVendaDto>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            CreateMap<ItemVenda, ItemVendaDto>(); 
        }
    }
}
