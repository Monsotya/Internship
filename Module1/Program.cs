using System;

namespace Name
{
public enum LengthType {Kilometer, Meter, Centimeter};
public class Program{
    public static void Main(){
        var convertedList = MyConverter.Convert(66.0, LengthType.Meter);
        foreach(var value in convertedList){
            Console.WriteLine(value.Key + " " +  value.Value);
        }
    }
}

public static class MyConverter{
    public static Dictionary<LengthType, double> Convert(double length, LengthType lengthType){
        Dictionary<LengthType, double> result = new Dictionary<LengthType, double>();
        switch(lengthType){
            case LengthType.Kilometer:
                result.Add(LengthType.Kilometer, length);
                result.Add(LengthType.Meter, length/1000);
                result.Add(LengthType.Centimeter, length/100000);
                break;
            case LengthType.Meter:
                result.Add(LengthType.Kilometer, length * 1000);
                result.Add(LengthType.Meter, length);
                result.Add(LengthType.Centimeter, length/100);
                break;
            case LengthType.Centimeter:
                result.Add(LengthType.Kilometer, length * 100000);
                result.Add(LengthType.Meter, length * 100);
                result.Add(LengthType.Centimeter, length);
                break;


        }
        return result;
    }
}
}
