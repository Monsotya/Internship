public class GalaxySector{
    private SolarSystem[] _solarSystems;

    public GalaxySector(SolarSystem[] solarSystems){
        this.SolarSystem = solarSystems;
    }
    public  SolarSystem[] SolarSystems{get;set;}

    public Planet[] PlanetsWithMoons(){
        Planet[] planets;

        foreach (SolarSystem solarSystem in _solarSystems ){
            planets += solarSystem.PlanetsWithMoons();
        }

        return planets;
    }

    public Planet[] HabitablePlanets(){
        Planet[] planets;

        foreach (SolarSystem solarSystem in _solarSystems ){
            planets += solarSystem.HabitablePlanets();
        }

        return planets;

    }
}