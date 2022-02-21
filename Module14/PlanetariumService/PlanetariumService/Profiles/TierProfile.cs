using AutoMapper;
using PlanetariumModels;
using PlanetariumServices.Models;

namespace PlanetariumService.Profiles
{
    public class TierProfile : Profile
    {
        public TierProfile()
        {
            CreateMap<Tier, TierUI>();
            CreateMap<TierUI, Tier>();
        }
    }
}
