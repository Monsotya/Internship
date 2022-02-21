using AutoMapper;
using PlanetariumModels;
using PlanetariumServices.Models;

namespace PlanetariumService.Profiles
{
    public class PerformanceProfile : Profile
    {
        public PerformanceProfile()
        {
            CreateMap<Performance, PerformanceUI>();
            CreateMap<PerformanceUI, Performance>();
            CreateMap<List<PerformanceUI>, List<Performance>>();
            CreateMap<List<Performance>, List<PerformanceUI>>();
        }
    }
}
