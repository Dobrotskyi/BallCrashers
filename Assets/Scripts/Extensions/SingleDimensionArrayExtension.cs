using System;

public static class SingleDimensionArrayExtension
{
    public static int MinValueIndex<T>(this T[] array) where T : IComparable
    {
        int i = 0;
        T minValue = array[i];
        int minValueIndex = i;
        for (; i < array.Length; i++)
        {
            if (array[i].CompareTo(minValue) < 0)
            {
                minValue = array[i];
                minValueIndex = i;
            }
        }

        return minValueIndex;
    }
}
