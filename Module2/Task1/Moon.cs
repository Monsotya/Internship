using System;
public class Moon{
    private int _age;
    private String _name;

    public Moon(int age, String name){
        this.Age = age;
        this.Name = name;
    }
    public int Age {
        get => _age;
        set
        {
            if (value < 0){
                throw new ArgumentOutOfRangeException();
            }

            _age = value;
        }
    }
    public string Name
    {
        get => this._name;
        set => this._name = value;
    }

    public override string ToString()
    {
        return $"{this._name}";
    }
}