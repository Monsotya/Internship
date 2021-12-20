using System;


public class Task1{

    public static void Main(){
        SolarSystem[] solarSystems;
        for (int i = 0; i < 15; i++){
            solarSystems += CreateSolarSystem();
        }
        GalaxySector galaxySector = new GalaxySector(solarSystems);*/

    }
    public static SolarSystem CreateSolarSystem()
    {
        Star[] stars;
        Planet[] planets;

        for(int i = 0; i < Random.Next(1,4); i++){
            stars += new Star(Random.Next(1000, 10000));
        }

        for (int i = 0; i < Random.Next(0,10); i++){
            Moon[] moons;
            for (int i = 0; i < Random.Next(0,10); i++){
                moons += new Moon(Random.Next(500, 5000));
            }
            planets += new Planet(Random.Next(0, 100), moons, Random.Next(1000, 10000));
        }

        return new SolarSystem(stars, planets);

    }
}