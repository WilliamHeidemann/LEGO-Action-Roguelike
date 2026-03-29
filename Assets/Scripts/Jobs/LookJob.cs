using Unity.Burst;
using UnityEngine;
using UnityEngine.Jobs;

namespace Jobs
{
    [BurstCompile]
    public struct LookJob : IJobParallelForTransform
    {
        public Vector3 Target;

        public void Execute(int index, TransformAccess transform)
        {
            Vector3 lookDirection = Target - transform.position;
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }
    }
}