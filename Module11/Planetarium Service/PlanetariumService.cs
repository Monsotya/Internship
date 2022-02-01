using PlanetariumServiceInterface;

namespace Planetarium_Service
{
    class PlanetariumService
    {
        public IPlanetariumService Service { get; set;}
        public PlanetariumService(IPlanetariumService obj)
        {
            Service = obj;
        }
    }
}
