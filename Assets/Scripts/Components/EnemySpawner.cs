using Entities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Components
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private Transform _spawnPoint;
        private EntityWorld _world;

        public void Initialize(EntityWorld entityWorld)
        {
            _world = entityWorld;
        }

        private void Start()
        {
            _ = StartSpawning();
        }

        private async Awaitable StartSpawning()
        {
            while (true)
            {
                if (PauseManager.PauseMode == PauseMode.Playing)
                {
                    SpawnWave();
                }

                await Awaitable.WaitForSecondsAsync(1);
            }
        }

        public void SpawnWave()
        {
            const int numberOfEnemies = 5;
            for (int i = 0; i < numberOfEnemies; i++)
            {
                float ithOffset = (i - 2) * 2;
                float x = _spawnPoint.position.x + Random.value * 2 - 1f + ithOffset;
                float z = _spawnPoint.position.z + Random.value * 2 - 1f;
                Vector3 spawnPosition = new(x, _spawnPoint.position.y, z);
                GameObject enemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
                _world.Enemies.Add(new Enemy(enemy.transform));
            }
        }
    }
}