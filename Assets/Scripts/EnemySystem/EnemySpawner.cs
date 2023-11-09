using GamePlay.Entities.Controllers;
using GamePlay.EventSystem;
using GamePlay.RoomSystem.Rooms;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace GamePlay.EnemySystem
{
    public class EnemySpawner : MonoBehaviour
    {
        public Action<EnemyController> OnEnemySpawned;

        [SerializeField] private int _enemyCount;
        [SerializeField] private int _waveCount;
        [SerializeField] private EnemyController[] _enemyPrefabs;

        private Room _currentRoom;

        private void Start()
        {
            EventManager.Instance.AddListener<RoomSpawnAnimationFinishedEvent>(SetCurrentRoom);
        }

        private void OnDisable()
        {
            EventManager.Instance.RemoveListener<RoomSpawnAnimationFinishedEvent>(SetCurrentRoom);
        }

        private void SpawnEnemy()
        {
            var enemy = Instantiate(_enemyPrefabs[UnityEngine.Random.Range(0, _enemyPrefabs.Length)], GetRandomPositionOnNavmesh(), Quaternion.identity, transform);
            OnEnemySpawned?.Invoke(enemy);
        }

        private Vector3 GetRandomPositionOnNavmesh()
        {
            var radius = Mathf.Max(_currentRoom.Size.x, _currentRoom.Size.y);
            var rand = UnityEngine.Random.insideUnitSphere * radius;
            if (NavMesh.SamplePosition(rand, out NavMeshHit hit, radius, 1))
            {
                return hit.position;
            }
            return GetRandomPositionOnNavmesh();
        }

        private void SetCurrentRoom(object data)
        {
            if (data == null) return;
            _currentRoom = ((RoomSpawnAnimationFinishedEvent)data).CurrentRoom;

            StartCoroutine(SpawnEnemies());
        }

        private IEnumerator SpawnEnemies()
        {
            for (int i = 0; i < _waveCount; i++)
            {
                for (int j = 0; j < (float)_enemyCount / _waveCount; j++)
                {
                    SpawnEnemy();
                }

                yield return new WaitForSeconds(13f);
            }
        }
    }
}