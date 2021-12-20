using System;
using System.Text;

namespace Task1{
    public class Planet {
        private int _procentOfOxygen;
        private Moon[] _moons;
        private int _age;
        private String _name;
        public Planet(int procentOfOxygen, Moon[] moons, int age, String name) {
            this.ProcentOfOxygen = procentOfOxygen;
            this.Moons = moons;
            this.Age = age;
            this.Name = name;
        }

        public string Name {
            get => this._name;
            set => this._name = value;
        }
        public int Age {
            get => _age;
            set
            {
                if (value < 0) {
                    throw new ArgumentOutOfRangeException();
                }

                _age = value;
            }
        }
        public int ProcentOfOxygen {
            get => _procentOfOxygen;
            set {
                if (value < 0 || value > 100) {
                    throw new ArgumentOutOfRangeException("Procent of oxygen must be between 0 and 100");
                }

                _procentOfOxygen = value;
            }
        }

        public Moon[] Moons { 
            get => this._moons;
            set => this._moons = value; 
        }

        public Boolean IsHabitable() {
            return _procentOfOxygen >= 20;
        }

        public override string ToString()
        {
            if (this._moons.Length == 0)
            {
                return $"{this._name}";
            }
            StringBuilder moons = new StringBuilder();
            foreach (Moon moon in _moons)
            {
                moons.Append(moon.ToString() + ", ");
            }
            moons.Remove(moons.Length - 2, 1);
            return $"{this._name}. Moons: " + moons + $". Procents of of oxygen: {this._procentOfOxygen}" ;
        }
    } 
}