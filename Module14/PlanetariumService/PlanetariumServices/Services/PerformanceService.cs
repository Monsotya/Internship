using PlanetariumModels;
using PlanetariumRepositories;

namespace PlanetariumServices
{
    public class PerformanceService : IPerformanceService
    {
        private readonly IRepository<Performance> performanceRepository;
        public PerformanceService(IRepository<Performance> performanceRepository)
        {
            this.performanceRepository = performanceRepository;
        }
        public List<Performance> GetAll()
        {
            return performanceRepository.GetAll().ToList<Performance>();
        }
    }
}
