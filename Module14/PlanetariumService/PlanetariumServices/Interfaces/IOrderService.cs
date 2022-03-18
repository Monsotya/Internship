using PlanetariumModels;

namespace PlanetariumServices
{
    public interface IOrderService
    {
        Task<Orders> Add(Orders order);
        List<Orders> GetAll();
    }
}