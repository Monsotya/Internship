using System;
public class Planet{
    private int _procentOfOxygen;
    private Moon[] _moons;
    private int _age;

    public Planet(int procentOfOxygen, Moon[] moons, int age){
        this.ProcentOfOxygen = procentOfOxygen;
        this.Moons = moons;
        this.Age = age;
    }
    public int Age {
        set{
            /*if (value < 0){
                throw new ArgumentOutOfRangeException();
            }*/

            _age = value;
        }
        get;
    }
    public int ProcentOfOxygen{
        get => _procentOfOxygen;
        set {
            if (typeof(value) != typeof(_procentOfOxygen)){
                throw new TypeInitializationException();
            }
            if (value < 0 || value > 100){
                throw new ArgumentOutOfRangeException($"{nameof(value)} must be between 0 and 100.");
            }

            _procentOfOxygen = value;
        }
    }

    public Moon[] Moons{    get; set;   }

    public Boolean IsHabitable(){
        return _procentOfOxygen >=20;
    }
}