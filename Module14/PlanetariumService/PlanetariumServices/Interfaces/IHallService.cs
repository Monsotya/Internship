using PlanetariumModels;

namespace PlanetariumServices
{
    public interface IHallService
    {
        List<Hall> GetAll(); 

        public Hall GetById(int id);
    }
}