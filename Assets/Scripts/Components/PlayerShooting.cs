using System.Collections.Generic;
using System.Linq;
using Entities;
using Jobs;
using UnityEngine;
using UnityEngine.Jobs;

namespace Components
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private Transform _player;
        [SerializeField] private float _shootingSpeed = 1f;
        private EntityWorld _world;
        private ExperienceTracker _experienceTracker;

        public void Initialize(EntityWorld world, ExperienceTracker experienceTracker)
        {
            _world = world;
            _experienceTracker = experienceTracker;
        }

        private void Start()
        {
            _ = StartShooting();
        }

        private async Awaitable StartShooting()
        {
            while (true)
            {
                if (PauseManager.PauseMode == PauseMode.Playing)
                {
                    Vector3 position = new Vector3(_player.position.x, _player.position.y + 3f, _player.position.z);
                    GameObject projectile = Instantiate(_projectilePrefab, position, _player.rotation);
                    _world.Projectiles.Add(new Projectile(projectile.transform));
                }

                await Awaitable.WaitForSecondsAsync(1f);
            }
        }

        private void Update()
        {
            MoveProjectiles();
        }

        private void MoveProjectiles()
        {
            using TransformAccessArray transformAccessArray =
                new(_world.Projectiles.Select(p => p.Transform.transform).ToArray());

            MoveJob moveJob = new()
            {
                DeltaTime = Time.deltaTime,
                ZVelocity = 100f
            };

            moveJob.ScheduleByRef(transformAccessArray).Complete();

            ColliderData<Projectile>[] projectileColliders =
                _world.Projectiles.Select(p => new ColliderData<Projectile>(p, p.Transform.position))
                    .ToArray();

            ColliderData<Enemy>[] enemyColliders =
                _world.Enemies.Select(e => new ColliderData<Enemy>(e, e.Transform.position))
                    .ToArray();

            IEnumerable<(Enemy, Projectile)> collisions =
                CollisionSystem.GetCollisions(enemyColliders, projectileColliders);

            foreach ((Enemy enemy, Projectile projectile) in collisions)
            {
                enemy.Transform.gameObject.SetActive(false);
                _world.Enemies.Remove(enemy);
                projectile.Transform.gameObject.SetActive(false);
                _world.Projectiles.Remove(projectile);
                _experienceTracker.Gain(1);
            }
        }
    }
}