using PlanetariumService.Models;

namespace PlanetariumService.Services
{
    public interface IHallService
    {
        List<Hall> GetAll();

        public Hall GetById(int id);
    }
}