using System.Collections.Generic;
using GamePlay.Entities.Controllers;
using GamePlay.EventSystem;
using GamePlay.MovementSystem.PlayerMovements;
using UnityEngine;

namespace GamePlay.EnemySystem
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private PlayerMovement _player;

        private List<EnemyController> _spawnedEnemies = new();

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

        public void AddEnemy(EnemyController enemy)
        {
            enemy.Init(_player.transform);
            enemy.OnDeath += RemoveEnemy;
            _spawnedEnemies.Add(enemy);
        }

        public void RemoveEnemy(EnemyController enemy)
        {
            enemy.OnDeath -= RemoveEnemy;
            _spawnedEnemies.Remove(enemy);

            if(_spawnedEnemies.Count <= 0)
            {
                EventManager.Instance.TriggerEvent<AllEnemiesAreDeadEvent>();
            }
        }
    }
}