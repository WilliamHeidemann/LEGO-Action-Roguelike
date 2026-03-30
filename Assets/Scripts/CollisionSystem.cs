using System.Collections.Generic;
using UnityEngine;

public static class CollisionSystem
{
    public static IEnumerable<(T1, T2)> GetCollisions<T1, T2>(
        ColliderData<T1>[] colliderData1,
        ColliderData<T2>[] colliderData2)
    {
        foreach (ColliderData<T1> a in colliderData1)
        {
            foreach (ColliderData<T2> b in colliderData2)
            {
                const float distanceThreshold = 20f;
                if (Vector3.SqrMagnitude(a.Position - b.Position) < distanceThreshold)
                {
                    yield return (a.Carrier, b.Carrier);
                }
            }
        }
    }
}