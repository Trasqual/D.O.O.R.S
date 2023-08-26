using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace GamePlay.RoomSystem.Placeables
{
    public class PropFactory : MonoBehaviour
    {
        [SerializeField] public SerializedDictionary<string, GameObject> Prefabs = new();

        public GameObject GetRoom(Transform parent)
        {
            GameObject spawn = null;
            if (Prefabs.TryGetValue("room", out var prefab))
            {
                spawn = Instantiate(prefab, parent);
            }
            return spawn;
        }

        public GameObject GetFloor(Transform parent)
        {
            GameObject spawn = null;
            if (Prefabs.TryGetValue("floor", out var prefab))
            {
                spawn = Instantiate(prefab, parent);
            }
            return spawn;
        }

        public GameObject GetFullHeightWallCorner(Transform parent)
        {
            GameObject spawn = null;
            if (Prefabs.TryGetValue("fullHeightWallCorner", out var prefab))
            {
                spawn = Instantiate(prefab, parent);
            }
            return spawn;
        }

        public GameObject GetShortWallCorner(Transform parent)
        {
            GameObject spawn = null;
            if (Prefabs.TryGetValue("shortWallCorner", out var prefab))
            {
                spawn = Instantiate(prefab, parent);
            }
            return spawn;
        }

        public GameObject GetFullHeightWall(Transform parent)
        {
            GameObject spawn = null;
            if (Prefabs.TryGetValue("fullHeightWall", out var prefab))
            {
                spawn = Instantiate(prefab, parent);
            }
            return spawn;
        }

        public GameObject GetShortWall(Transform parent)
        {
            GameObject spawn = null;
            if (Prefabs.TryGetValue("shortWall", out var prefab))
            {
                spawn = Instantiate(prefab, parent);
            }
            return spawn;
        }

        public GameObject GetFullHeightDoor(Transform parent)
        {
            GameObject spawn = null;
            if (Prefabs.TryGetValue("fullHeightDoor", out var prefab))
            {
                spawn = Instantiate(prefab, parent);
            }
            return spawn;
        }

        public GameObject GetShortDoor(Transform parent)
        {
            GameObject spawn = null;
            if (Prefabs.TryGetValue("shortDoor", out var prefab))
            {
                spawn = Instantiate(prefab, parent);
            }
            return spawn;
        }
    }
}