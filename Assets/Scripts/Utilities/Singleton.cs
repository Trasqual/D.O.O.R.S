using UnityEngine;

namespace GamePlay.Utilities
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance;

        protected virtual void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }

            Instance = this as T;
        }
    }
}