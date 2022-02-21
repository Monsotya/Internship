using AutoMapper;
using PlanetariumModels;
using PlanetariumServices.Models;

namespace PlanetariumService.Profiles
{
    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            CreateMap<Ticket, TicketUI>();
            CreateMap<TicketUI, Ticket>();
            CreateMap<List<TicketUI>, List<Ticket>>();
            CreateMap<List<Ticket>, List<TicketUI>>();
        }
    }
}
