using System;

public class Choice
{
    private readonly Action _one;
    private readonly Action _two;
    private readonly Action _three;

    public Choice(Action one, Action two, Action three)
    {
        _one = one;
        _two = two;
        _three = three;
    }

    public void Invoke(int index)
    {
        Action action = index switch
        {
            0 => _one,
            1 => _two,
            2 => _three,
            _ => throw new ArgumentOutOfRangeException()
        };
        action?.Invoke();
    }
}