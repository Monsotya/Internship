using System;
using System.Collections.Generic;

namespace Task1
{
    public class GalaxySector
    {
        private SolarSystem[] _solarSystems;
        private String _name;

        public GalaxySector(SolarSystem[] solarSystems, String name)
        {
            this.SolarSystems = solarSystems;
            this.Name = name;
        }
        public SolarSystem[] SolarSystems
        {
            get => this._solarSystems;
            set => this._solarSystems = value;
        }
        public string Name
        {
            get => this._name;
            set => this._name = value;
        }

        public Planet[] PlanetsWithMoons()
        {
            List<Planet> planets = new List<Planet>();

            foreach (SolarSystem solarSystem in _solarSystems)
            {
                foreach (Planet planet in solarSystem.PlanetsWithMoons())
                {
                    planets.Add(planet);
                }
            }

            return planets.ToArray();
        }

        public Planet[] HabitablePlanets()
        {
            List<Planet> planets = new List<Planet>();

            foreach (SolarSystem solarSystem in this._solarSystems)
            {
                foreach(Planet planet in solarSystem.HabitablePlanets())
                {
                    planets.Add(planet);
                }
            }

            return planets.ToArray();

        }
    }
}