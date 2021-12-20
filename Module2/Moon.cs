public class Moon{
    private int _age;

    public Moon(int age){
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
}