using API.Dtos;
using AutoMapper;
using Domain.Entities;

namespace API.Profiles
{
    public class BoardProfile : Profile
    {
        public BoardProfile()
        {
            CreateMap<Venda, RegistrarVendaDto>();
        }
    }
}
