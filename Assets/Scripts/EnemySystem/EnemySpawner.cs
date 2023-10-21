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
        public Action<Enemy> OnEnemySpawned;

        [SerializeField] private Enemy _enemyPrefab;

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
            var enemy = Instantiate(_enemyPrefab, GetRandomPositionOnNavmesh(), Quaternion.identity, transform);
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
            _currentRoom = ((RoomSpawnAnimationFinishedEvent)data).CurrentRoom;

            StartCoroutine(SpawnEnemies());
        }

        private IEnumerator SpawnEnemies()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    SpawnEnemy();
                }

                yield return new WaitForSeconds(13f);
            }
        }
    }
}