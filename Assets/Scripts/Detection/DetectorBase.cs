using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.DetectionSystem
{
    public class DetectorBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] protected SphereCollider _collider;

        protected List<T> _detectedsList = new();
        public int DetectedCount => _detectedsList.Count;
        public List<T> Detecteds => _detectedsList;

        public void UpdateSize(float range)
        {
            _collider.radius = range;
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out T detected))
            {
                if (!_detectedsList.Contains(detected))
                {
                    OnDetected(detected);
                }
            }
        }

        protected virtual void OnDetected(T detected)
        {
            _detectedsList.Add(detected);
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out T detected))
            {
                if (_detectedsList.Contains(detected))
                    OnDetectedExit(detected);
            }
        }

        protected virtual void OnDetectedExit(T detected)
        {
            RemoveDetected(detected);
        }

        protected virtual void RemoveDetected(T detected)
        {
            _detectedsList.Remove(detected);
        }

        public T GetClosestDetected()
        {
            var closestDist = float.MaxValue;
            T closestDetected = null;

            for (int i = 0; i < _detectedsList.Count; i++)
            {
                var sqrMgt = (transform.position - _detectedsList[i].transform.position).sqrMagnitude;
                if (sqrMgt < closestDist)
                {
                    closestDist = sqrMgt;
                    closestDetected = _detectedsList[i];
                }
            }

            return closestDetected;
        }
    }
}