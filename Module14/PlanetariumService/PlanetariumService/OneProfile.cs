using AutoMapper;
using PlanetariumModels;
using PlanetariumService.Models;

namespace PlanetariumService.Profiles
{
    public class OneProfile : Profile
    {
        public OneProfile()
        {
            CreateMap<Hall, HallUI>()
                .ForMember(d => d.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(d => d.HallName, opts => opts.MapFrom(src => src.HallName))
                .ForMember(d => d.Posters, opts => opts.MapFrom(src => src.Posters))
                .ForMember(d => d.Capacity, opts => opts.MapFrom(src => src.Capacity));                
            CreateMap<HallUI, Hall>()
                .ForMember(d => d.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(d => d.HallName, opts => opts.MapFrom(src => src.HallName))
                .ForMember(d => d.Posters, opts => opts.MapFrom(src => src.Posters))
                .ForMember(d => d.Capacity, opts => opts.MapFrom(src => src.Capacity));

            CreateMap<Poster, PosterUI>(MemberList.Source)
                .ForMember(d => d.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(d => d.Performance, opts => opts.MapFrom(src => src.Performance))
                .ForMember(d => d.PerformanceId, opts => opts.MapFrom(src => src.PerformanceId))
                .ForMember(d => d.Hall, opts => opts.MapFrom(src => src.Hall))
                .ForMember(d => d.HallId, opts => opts.MapFrom(src => src.HallId))
                .ForMember(d => d.Price, opts => opts.MapFrom(src => src.Price))
                .ForMember(d => d.Tickets, opts => opts.MapFrom(src => src.Tickets))
                .ForMember(d => d.DateOfEvent, opts => opts.MapFrom(src => src.DateOfEvent));
            CreateMap<PosterUI, Poster>()
                .ForMember(d => d.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(d => d.Performance, opts => opts.MapFrom(src => src.Performance))
                .ForMember(d => d.PerformanceId, opts => opts.MapFrom(src => src.PerformanceId))
                .ForMember(d => d.Hall, opts => opts.MapFrom(src => src.Hall))
                .ForMember(d => d.HallId, opts => opts.MapFrom(src => src.HallId))
                .ForMember(d => d.Price, opts => opts.MapFrom(src => src.Price))
                .ForMember(d => d.Tickets, opts => opts.MapFrom(src => src.Tickets))
                .ForMember(d => d.DateOfEvent, opts => opts.MapFrom(src => src.DateOfEvent));

            CreateMap<Orders, OrdersUI>()
                .ForMember(d => d.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(d => d.ClientSurname, opts => opts.MapFrom(src => src.ClientSurname))
                .ForMember(d => d.ClientName, opts => opts.MapFrom(src => src.ClientName))
                .ForMember(d => d.Tickets, opts => opts.MapFrom(src => src.Tickets))
                .ForMember(d => d.Email, opts => opts.MapFrom(src => src.Email))
                .ForMember(d => d.DateOfOrder, opts => opts.MapFrom(src => src.DateOfOrder));
            CreateMap<OrdersUI, Orders>()
                .ForMember(d => d.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(d => d.ClientSurname, opts => opts.MapFrom(src => src.ClientSurname))
                .ForMember(d => d.ClientName, opts => opts.MapFrom(src => src.ClientName))
                .ForMember(d => d.Tickets, opts => opts.MapFrom(src => src.Tickets))
                .ForMember(d => d.Email, opts => opts.MapFrom(src => src.Email))
                .ForMember(d => d.DateOfOrder, opts => opts.MapFrom(src => src.DateOfOrder));

            CreateMap<Performance, PerformanceUI>()
                .ForMember(d => d.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(d => d.Title, opts => opts.MapFrom(src => src.Title))
                .ForMember(d => d.Duration, opts => opts.MapFrom(src => src.Duration))
                .ForMember(d => d.EventDescription, opts => opts.MapFrom(src => src.EventDescription))
                .ForMember(d => d.Posters, opts => opts.MapFrom(src => src.Posters));
            CreateMap<PerformanceUI, Performance>()
                .ForMember(d => d.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(d => d.Title, opts => opts.MapFrom(src => src.Title))
                .ForMember(d => d.Duration, opts => opts.MapFrom(src => src.Duration))
                .ForMember(d => d.EventDescription, opts => opts.MapFrom(src => src.EventDescription))
                .ForMember(d => d.Posters, opts => opts.MapFrom(src => src.Posters));

            CreateMap<Ticket, TicketUI>()
                .ForMember(d => d.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(d => d.Place, opts => opts.MapFrom(src => src.Place))
                .ForMember(d => d.Poster, opts => opts.MapFrom(src => src.Poster))
                .ForMember(d => d.PosterId, opts => opts.MapFrom(src => src.PosterId))
                .ForMember(d => d.Tier, opts => opts.MapFrom(src => src.Tier))
                .ForMember(d => d.Order, opts => opts.MapFrom(src => src.Order))
                .ForMember(d => d.OrderId, opts => opts.MapFrom(src => src.OrderId))
                .ForMember(d => d.TicketStatus, opts => opts.MapFrom(src => src.TicketStatus))
                .ForMember(d => d.TierId, opts => opts.MapFrom(src => src.TierId));
            CreateMap<TicketUI, Ticket>()
                .ForMember(d => d.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(d => d.Place, opts => opts.MapFrom(src => src.Place))
                .ForMember(d => d.Poster, opts => opts.MapFrom(src => src.Poster))
                .ForMember(d => d.PosterId, opts => opts.MapFrom(src => src.PosterId))
                .ForMember(d => d.Tier, opts => opts.MapFrom(src => src.Tier))
                .ForMember(d => d.Order, opts => opts.MapFrom(src => src.Order))
                .ForMember(d => d.OrderId, opts => opts.MapFrom(src => src.OrderId))
                .ForMember(d => d.TicketStatus, opts => opts.MapFrom(src => src.TicketStatus))
                .ForMember(d => d.TierId, opts => opts.MapFrom(src => src.TierId));

            CreateMap<Tier, TierUI>()
                .ForMember(d => d.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(d => d.Surcharge, opts => opts.MapFrom(src => src.Surcharge))
                .ForMember(d => d.Tickets, opts => opts.MapFrom(src => src.Tickets))
                .ForMember(d => d.TierName, opts => opts.MapFrom(src => src.TierName));
            CreateMap<TierUI, Tier>()
                .ForMember(d => d.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(d => d.Surcharge, opts => opts.MapFrom(src => src.Surcharge))
                .ForMember(d => d.Tickets, opts => opts.MapFrom(src => src.Tickets))
                .ForMember(d => d.TierName, opts => opts.MapFrom(src => src.TierName));
        }
    }
}
