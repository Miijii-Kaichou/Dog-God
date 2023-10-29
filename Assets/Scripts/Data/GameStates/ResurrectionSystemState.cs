using System;

[Serializable]
public sealed class ResurrectionSystemState
{
    public ResurrectionSystemState(int lives)
    {
        Lives = lives;
    }

    public int Lives { get; }
}
