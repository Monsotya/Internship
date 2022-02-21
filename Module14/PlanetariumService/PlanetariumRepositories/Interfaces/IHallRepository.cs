using PlanetariumModels;

namespace PlanetariumRepositories
{
    public interface IHallRepository
    {
        Task<Hall> GetByIdAsync(int id);

        public List<Hall> GetAll();
    }
}