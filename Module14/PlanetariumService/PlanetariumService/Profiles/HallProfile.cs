using AutoMapper;
using PlanetariumModels;
using PlanetariumServices.Models;

namespace PlanetariumService.Profiles
{
    public class HallProfile : Profile
    {
        public HallProfile()
        {
            CreateMap<Hall, HallUI>();
        }
    }
}
