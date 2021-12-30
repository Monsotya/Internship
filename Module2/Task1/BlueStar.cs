using System;

namespace Task1
{
    public class BlueStar : IStar
    {

        private int _age;
        private String _name;
        public BlueStar(int age, String name)
        {
            this.Age = age;
            this.Name = name;
        }

        public string Name { get => _name; set => _name = value; }

        public int Age
        {
            get => _age;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Age must be a positive number!");
                }

                _age = value;
            }
        }
        public override string ToString()
        {
            return $"Blue star: {this._name}";
        }
    }
}