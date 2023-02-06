using System;
using UnityEngine;

[System.Serializable]
public class Navigation<T>
{
    [SerializeField]
    T[] array;

    public T[] Array => array;

    public int Size { get; private set; } = 0;

    public Navigation()
    {
        array = new T[Size + 1];
    }

    public T Stretch(T data)
    {

        Size += 1;
        array[Size - 1] = data;
        Resize(Size);
        return array[Size - 1];
    }

    public T Condense(int distance)
    {
        Size -= distance;
        T prev = array[Size];
        Resize(Size);
        return prev;
    }

    public T TopLevel => Array[Size];

    public T Find(Predicate<T> condiiton)
    {
        for (int i = 0; i < Size; i++)
        {
            var targetElement = Array[i];
            var found = condiiton.Invoke(targetElement);
            if (found) return targetElement;
        }
        return TopLevel;
    }

    int Resize(int newSize)
    {
        try
        {
            T[] keeper = array;

            array = new T[newSize + 1];

            if (keeper.Length > 0)
            {
                for (int i = 0; i < newSize; i++)
                {
                    array[i] = keeper[i];
                }
            }

            return newSize;
        }
        catch
        {
            return 0;
        }
    }
}
