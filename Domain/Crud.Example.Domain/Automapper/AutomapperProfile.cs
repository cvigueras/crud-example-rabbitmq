using AutoMapper;
using Crud.Example.Domain.Dtos;
using Crud.Example.Domain.Entities;

namespace Crud.Example.Domain.Automapper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<DealerDto, Dealer>();
        }
    }
}
