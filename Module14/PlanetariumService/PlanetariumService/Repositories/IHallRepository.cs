using PlanetariumService.Models;

namespace PlanetariumService.Repositories
{
    public interface IHallRepository
    {
        Task<Hall> GetByIdAsync(int id);

        public List<Hall> GetAll();
    }
}