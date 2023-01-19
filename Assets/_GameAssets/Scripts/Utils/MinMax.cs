using System.Collections.Generic;

[System.Serializable]
public class MinMax<T>
{
    public T min;
    public T max;

    public MinMax(T _min, T _max)
    {
        this.min = _min;
        this.max = _max;
    }
}
