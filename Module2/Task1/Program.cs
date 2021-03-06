using System;
using System.Collections.Generic;

namespace Task1
{
    public class Task1
    {

        public static void Main()
        {
            SolarSystem[] solarSystems = new SolarSystem[15];
            
            for (int i = 0; i < 15; i++)
            {
                solarSystems[i] = CreateSolarSystem();
            }
            GalaxySector galaxySector = new GalaxySector(solarSystems, "Galaxy sector");
            Planet[] planets = galaxySector.PlanetsWithMoons();
            RedStar[] redStars = galaxySector.FindRedStars();
            Console.WriteLine("Red stars:\n\n");
            foreach (RedStar star in redStars)
            {
                Console.WriteLine(star.ToString() + " ");
            }
            Console.WriteLine("\n\nPlanets with moons:\n\n");
            foreach (Planet planet in planets)
            {
                Console.WriteLine(planet.ToString() + " ");
            }

        }
        public static SolarSystem CreateSolarSystem()
        {
            List<IStar> stars = new List<IStar>();
            List<Planet> planets = new List<Planet>();
            Random random = new Random();

            for (int i = 0; i < random.Next(1, 4); i++)
            {
                if (random.Next(0, 2) != 0)
                {
                    stars.Add(new RedStar(random.Next(1000, 10000), "RedStar" + random.Next(1, 1000).ToString()));
                }
                else
                {
                    stars.Add(new BlueStar(random.Next(1000, 10000), "BlueStar" + random.Next(1, 1000).ToString()));
                }
            }

            for (int i = 0; i < random.Next(0, 10); i++)
            {
                List<Moon> moons = new List<Moon>();
                for (int j = 0; j < random.Next(0, 10); j++)
                {
                    moons.Add(new Moon(random.Next(500, 5000), "Moon" + random.Next(1, 1000).ToString()));
                }
                planets.Add(new Planet(random.Next(0, 100), moons.ToArray(), random.Next(1000, 10000), 
                    "Planet" + random.Next(1, 1000).ToString()));
            }

            return new SolarSystem(stars.ToArray(), planets.ToArray(), "Solar System " + random.Next(1, 1000).ToString());

        }
    }
}