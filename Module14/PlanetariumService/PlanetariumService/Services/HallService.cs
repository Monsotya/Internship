using PlanetariumService.Models;
using PlanetariumService.Repositories;

namespace PlanetariumService.Services
{
    public class HallService : IHallService
    {
        private readonly IHallRepository hallRepository;
        public HallService(IHallRepository hallRepository)
        {
            this.hallRepository = hallRepository;
        }
        public List<Hall> GetAll()
        {
            return hallRepository.GetAll();
        }

        public Hall GetById(int id)
        {
            return hallRepository.GetByIdAsync(id).Result;
        }
    }
}
