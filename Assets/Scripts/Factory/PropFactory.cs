using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.RoomSystem.Placeables
{
    public class PropFactory : MonoBehaviour
    {
        [SerializeField] private SerializedDictionary<string, GameObject> _prefabs = new();

        [SerializeField] private List<GameObject> _grassAndPebbles = new List<GameObject>();

        public GameObject GetRoom(Transform parent)
        {
            GameObject spawn = null;
            if (_prefabs.TryGetValue("room", out var prefab))
            {
                spawn = Instantiate(prefab, parent);
            }
            return spawn;
        }

        public GameObject GetFloor(int width, int height, Transform parent)
        {
            GameObject spawn = null;
            if (_prefabs.TryGetValue("floor", out var prefab))
            {
                spawn = Instantiate(prefab, parent);
                spawn.transform.localScale = new Vector3(width, 1f, height);
                SpawnRandomFloorDebris(width, height, spawn.transform);
            }
            return spawn;
        }

        private void SpawnRandomFloorDebris(int width, int height, Transform floor)
        {
            int amountToCreate = Mathf.CeilToInt(width * height / 100f);

            for (int i = 0; i < amountToCreate; i++)
            {
                float randX = Random.Range(-(width - 2f) / 2f, (width - 2f) / 2f);
                float randY = Random.Range(-(height - 2f) / 2f, (height - 2f) / 2f);
                int randomIndex = Random.Range(0, _grassAndPebbles.Count);

                GameObject debris = Instantiate(_grassAndPebbles[randomIndex]);

                debris.transform.SetParent(floor);
                debris.transform.position = new Vector3(randX, 0f, randY);
                debris.transform.localEulerAngles = new Vector3(0f, Random.Range(0f, 360f), 0f);
            }
        }

        public GameObject GetFullHeightWallCorner(Transform parent)
        {
            GameObject spawn = null;
            if (_prefabs.TryGetValue("fullHeightWallCorner", out var prefab))
            {
                spawn = Instantiate(prefab, parent);
            }
            return spawn;
        }

        public GameObject GetShortWallCorner(Transform parent)
        {
            GameObject spawn = null;
            if (_prefabs.TryGetValue("shortWallCorner", out var prefab))
            {
                spawn = Instantiate(prefab, parent);
            }
            return spawn;
        }

        public GameObject GetFullHeightWall(Transform parent)
        {
            GameObject spawn = null;
            if (_prefabs.TryGetValue("fullHeightWall", out var prefab))
            {
                spawn = Instantiate(prefab, parent);
            }
            return spawn;
        }

        public GameObject GetShortWall(Transform parent)
        {
            GameObject spawn = null;
            if (_prefabs.TryGetValue("shortWall", out var prefab))
            {
                spawn = Instantiate(prefab, parent);
            }
            return spawn;
        }

        public GameObject GetFullHeightDoor(Transform parent)
        {
            GameObject spawn = null;
            if (_prefabs.TryGetValue("fullHeightDoor", out var prefab))
            {
                spawn = Instantiate(prefab, parent);
            }
            return spawn;
        }

        public GameObject GetShortDoor(Transform parent)
        {
            GameObject spawn = null;
            if (_prefabs.TryGetValue("shortDoor", out var prefab))
            {
                spawn = Instantiate(prefab, parent);
            }
            return spawn;
        }
    }
}