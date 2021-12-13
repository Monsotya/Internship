using System;
using ConvertLength;

namespace Name
{
public class Program{
    public static void Main(){
        var convertedList = MyConverter.Convert(66.0, LengthType.Meter);
        foreach(var value in convertedList){
            Console.WriteLine(value.Key + " " +  value.Value);
        }
    }
}
}
