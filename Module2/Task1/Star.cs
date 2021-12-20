using System;

namespace Task1
{
    public class Star
    {
        private int _age;
        private String _name;

        public Star(int age, String name)
        {
            this.Age = age;
            this.Name = name;
        }
        public string Name { get; set; }

        public int Age
        {
            get => _age;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }

                _age = value;
            }
        }
        public override string ToString()
        {
            return $"{this._name}";
        }
    }
}