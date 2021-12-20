public class Star{
    private int _age;

    public Star(int age){
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