using AutoMapper;
using Entities.Concretes;
using Entities.DTOs.Car;

namespace Business.MappingProfiles
{
    // builder.Services.AddSingelton<IProfile,CarMappingProfile>
    // Reflection
    // Uygulamada Profile classını inherit eden tüm classları DI Containera ekle
    public class CarMappingProfiles : Profile
    {
        public CarMappingProfiles()
        {
            CreateMap<CarForAddDto, Car>().ReverseMap();
            CreateMap<CarForListingDto, Car>().ReverseMap().ForMember(c=>c.ColorName, opt => opt.MapFrom(o=>o.Color.Name));
            CreateMap<CarForUpdateDto, Car>().ReverseMap();
        }
    }
}
