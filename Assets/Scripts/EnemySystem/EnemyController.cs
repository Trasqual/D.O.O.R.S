using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.EnemySystem
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private List<Enemy> _spawnedEnemies = new();

        private void Awake()
        {
            _enemySpawner.OnEnemySpawned += AddEnemy;
        }

        private void Update()
        {
            foreach (var enemy in _spawnedEnemies)
            {
                enemy.UpdateEnemy();
            }
        }

        public void AddEnemy(Enemy enemy)
        {
            _spawnedEnemies.Add(enemy);
        }

        public void RemoveEnemy(Enemy enemy)
        {
            _spawnedEnemies.Remove(enemy);
        }
    }
}