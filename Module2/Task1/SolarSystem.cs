using System;
using System.Collections.Generic;
using System.Text;

namespace Task1
{
    public class SolarSystem
    {
        private Star[] _stars;
        private Planet[] _planets;
        private String _name;


        public SolarSystem(Star[] stars, Planet[] planets, String name)
        {
            this.Stars = stars;
            this.Planets = planets;
            this.Name = name;
        }

        public Planet[] PlanetsWithMoons()
        {
            List<Planet> planets = new List<Planet>();

            foreach (Planet planet in _planets)
            {
                if (planet.Moons.Length != 0)
                {
                    planets.Add(planet);
                }
            }
            return planets.ToArray();
        }

        public Planet[] HabitablePlanets()
        {
            List<Planet> planets = new List<Planet>();

            foreach (Planet planet in _planets)
            {
                if (planet.IsHabitable())
                {
                    planets.Add(planet);
                }
            }
            return planets.ToArray();
        }

        public Planet[] Planets
        {
            get => this._planets;
            set => this._planets = value;
        }
        public string Name
        {
            get => this._name;
            set => this._name = value;
        }

        public Star[] Stars
        {
            get => _stars;
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentOutOfRangeException();
                }

                _stars = value;
            }

        }
        public override string ToString()
        {
            StringBuilder stars = new StringBuilder("Star");
            if (this._stars.Length == 1)
            {
                stars.Append(": " + this._stars[0].ToString());
            }
            else
            {
                stars.Append("s: ");
                foreach (Star star in this._stars)
                {
                    stars.Append(star.ToString() + ", ");
                }
                stars.Remove(stars.Length - 1, 1);
            }

            if (this._planets.Length == 0)
            {
                return $"{this._name}. " + stars;
            }
            StringBuilder moons = new StringBuilder();
            StringBuilder planets = new StringBuilder("Planet");
            if (this._planets.Length == 1)
            {
                stars.Append(": " + this._planets[0].ToString());
            }
            else
            {
                stars.Append("s: ");
                foreach (Planet planet in this._planets)
                {
                    stars.Append(planet.ToString() + ", ");
                }
                stars.Remove(stars.Length - 2, 1);
            }
            return $"{this._name}. " + stars + ". " + planets;
        }

    }
    
}