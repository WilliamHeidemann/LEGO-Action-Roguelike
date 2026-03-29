using Unity.Burst;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Jobs;

namespace Jobs
{
    [BurstCompile]
    public struct MoveJob : IJobParallelForTransform
    {
        [ReadOnly] public float DeltaTime;
        [ReadOnly] public float ZVelocity;
    
        public void Execute(int index, TransformAccess transform)
        {
            transform.position += new Vector3(0, 0, ZVelocity * DeltaTime);
        }
    }
}