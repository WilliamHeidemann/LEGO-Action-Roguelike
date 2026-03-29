using UnityEngine;

public readonly struct ColliderData<T>
{
    public readonly T Carrier;
    public readonly Vector3 Position;

    public ColliderData(T carrier, Vector3 position)
    {
        Carrier = carrier;
        Position = position;
    }
}