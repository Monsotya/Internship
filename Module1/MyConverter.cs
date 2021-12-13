using System;
namespace ConvertLength{

    public enum LengthType {Kilometer, Meter, Centimeter};
    public static class MyConverter{
    public static Dictionary<LengthType, double> Convert(double length, LengthType lengthType){
        Dictionary<LengthType, double> result = new Dictionary<LengthType, double>();
        result = lengthType switch{
            LengthType.Kilometer => ApplyLength(length, length * 1000, length * 100000),
            LengthType.Meter => ApplyLength(length / 1000, length, length * 100),
            LengthType.Centimeter => ApplyLength(length / 100000, length / 100, length),
            _ => throw new NotImplementedException()
        };
        return result;
    }
    public static Dictionary<LengthType, double> ApplyLength(double kmLength, double mLength, double cmLength){
        Dictionary<LengthType, double> result = new Dictionary<LengthType, double>();
        result.Add(LengthType.Kilometer, kmLength);
        result.Add(LengthType.Meter, mLength);
        result.Add(LengthType.Centimeter, cmLength);
        return result;
    }
    
}
}