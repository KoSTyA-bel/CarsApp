using AutoMapper;
using CarsApp.Businesslogic.Entities;
using CarsApp.Models;

namespace CarsApp
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Car, CarViewModel>().ReverseMap();
            CreateMap<Engine, EngineViewModel>().ReverseMap();
        }
    }
}
