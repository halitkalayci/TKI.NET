using AutoMapper;
using Entities.Concretes;
using Entities.DTOs.Car;

namespace Business.MappingProfiles
{
    public class CarMappingProfiles : Profile
    {
        public CarMappingProfiles()
        {
            CreateMap<CarForAddDto, Car>().ReverseMap();
        }
    }
}
