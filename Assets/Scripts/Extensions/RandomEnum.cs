using System;

public static class RandomEnum
{
    public static T GetRandom<T>() where T : Enum
    {
        Array values = Enum.GetValues(typeof(T));
        Random random = new Random();
        T randomEnum = (T)values.GetValue(random.Next(values.Length));
        return randomEnum;
    }
}
