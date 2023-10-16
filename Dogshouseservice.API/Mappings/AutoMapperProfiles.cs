using AutoMapper;
using Dogshouseservice.API.Models.Domain;
using Dogshouseservice.API.Models.DTO;

namespace Dogshouseservice.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Dog, DogDto>().ReverseMap();
            CreateMap<Dog, AddDogRequestDto>().ReverseMap();
        }
    }
}