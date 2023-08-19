using AYellowpaper.SerializedCollections;
using UnityEngine;

public class PrefabProvider : MonoBehaviour
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

    public GameObject GetWallCorner(Transform parent)
    {
        GameObject spawn = null;
        if (Prefabs.TryGetValue("wallCorner", out var prefab))
        {
            spawn = Instantiate(prefab, parent);
        }
        return spawn;
    }

    public GameObject GetWall(Transform parent)
    {
        GameObject spawn = null;
        if (Prefabs.TryGetValue("wall", out var prefab))
        {
            spawn = Instantiate(prefab, parent);
        }
        return spawn;
    }
}
