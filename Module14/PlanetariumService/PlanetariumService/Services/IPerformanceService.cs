using PlanetariumService.Models;

namespace PlanetariumService.Services
{
    public interface IPerformanceService
    {
        List<Performance> GetAll();
    }
}