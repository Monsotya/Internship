using AutoMapper;
using PlanetariumModels;
using PlanetariumServices.Models;

namespace PlanetariumService.Profiles
{
    public class PosterProfile : Profile
    {
        public PosterProfile()
        {
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
            CreateMap<List<PosterUI>, List<Poster>>()
                .ForMember(d => d.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(d => d.Performance, opts => opts.MapFrom(src => src.Performance))
                .ForMember(d => d.PerformanceId, opts => opts.MapFrom(src => src.PerformanceId))
                .ForMember(d => d.Hall, opts => opts.MapFrom(src => src.Hall))
                .ForMember(d => d.HallId, opts => opts.MapFrom(src => src.HallId))
                .ForMember(d => d.Price, opts => opts.MapFrom(src => src.Price))
                .ForMember(d => d.Tickets, opts => opts.MapFrom(src => src.Tickets))
                .ForMember(d => d.DateOfEvent, opts => opts.MapFrom(src => src.DateOfEvent));
            CreateMap<List<Poster>, List<PosterUI>>()
                .ForMember(d => d.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(d => d.Performance, opts => opts.MapFrom(src => src.Performance))
                .ForMember(d => d.PerformanceId, opts => opts.MapFrom(src => src.PerformanceId))
                .ForMember(d => d.Hall, opts => opts.MapFrom(src => src.Hall))
                .ForMember(d => d.HallId, opts => opts.MapFrom(src => src.HallId))
                .ForMember(d => d.Price, opts => opts.MapFrom(src => src.Price))
                .ForMember(d => d.Tickets, opts => opts.MapFrom(src => src.Tickets))
                .ForMember(d => d.DateOfEvent, opts => opts.MapFrom(src => src.DateOfEvent));
        }
    }
}
