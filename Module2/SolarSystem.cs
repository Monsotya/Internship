public class SolarSystem{
    private Star[] _stars;
    private Planet[] _planets;

    public SolarSystem(Star[] stars, Planet[] planets){
        this.Stars = stars;
        this.Planets = planets;
    }
    
    public Planet[] PlanetsWithMoons(){
        Planet[] planets;

        foreach (Planet planet in _planets ){
            if(planet.Planets.get().Length != 0){
                planets += planet;
            }
        }
        return planets;
    }

    public Planet[] HabitablePlanets(){
        Planet[] planets;

        foreach (Planet planet in _planets ){
            if(planet.IsHabitable()){
                planets += planet;
            }
        }
        return planets;
    }
    
    public Planet[] Planets{get;set;}
    public Star[] Stars{
        get => _stars;
        set{
            if (value.Length == 0){
                throw new ArgumentOutOfRangeException();
            }

            _stars = value;
        }

    }
    
}