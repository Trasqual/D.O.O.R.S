using UnityEngine;

namespace GamePlay.Entities
{
    public class Entity : MonoBehaviour
    {
        public EntityType EntityType;
    }

    public enum EntityType
    {
        None,
        Player,
        Enemy,
        Breakable
    }
}