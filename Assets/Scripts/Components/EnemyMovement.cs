using System.Linq;
using Jobs;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

namespace Components
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private float _moveSpeed;
        private EntityWorld _world;

        public void Initialize(EntityWorld world)
        {
            _world = world;
        }

        private void Update()
        {
            if (PauseManager.PauseMode != PauseMode.Playing) return;
            Move();
        }

        private void Move()
        {
            using TransformAccessArray transformAccessArray = 
                new(_world.Enemies.Select(e => e.Transform).ToArray());
            
            MoveJob moveJob = new()
            {
                DeltaTime = Time.deltaTime,
                ZVelocity = -_moveSpeed
            };
            
            JobHandle moveHandle = moveJob.ScheduleByRef(transformAccessArray);

            LookJob lookJob = new()
            {
                Target = _player.position
            };

            lookJob.ScheduleByRef(transformAccessArray, moveHandle).Complete();
        }
    }
}