using API.Dtos;
using AutoMapper;
using Domain.Entities;

namespace API.Profiles
{
    public class VendaProfile : Profile
    {
        public VendaProfile()
        {
            CreateMap<Venda, RegistrarVendaDto>();
        }
    }
}
