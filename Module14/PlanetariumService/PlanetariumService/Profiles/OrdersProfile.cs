using AutoMapper;
using PlanetariumModels;
using PlanetariumServices.Models;

namespace PlanetariumService.Profiles
{
    public class OrdersProdile : Profile
    {
        public OrdersProdile()
        {
            CreateMap<Orders, OrdersUI>();
            CreateMap<OrdersUI, Orders>();
        }
    }
}
