using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.RoomSystem.Placeables
{
    public class FullHeightWall : Wall
    {
        [SerializeField] private GameObject _torchPrefab;
        [SerializeField] private float _minTorchInterval;
        [SerializeField] private float _torchSpawnChance;

        private List<GameObject> _torches = new();

        public override void Initialize(float size)
        {
            base.Initialize(size);

            CreateTorches();
        }

        public override void PrepareForAnimation()
        {
            base.PrepareForAnimation();
            foreach (var torch in _torches)
            {
                torch.transform.localScale = Vector3.zero;
            }
        }

        protected override void PlaySpawnParticles()
        {
            base.PlaySpawnParticles();

            foreach (var torch in _torches)
            {
                torch.transform.localScale = Vector3.zero;
                torch.transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.OutBack);
            }
        }

        private void CreateTorches()
        {
            if (_minTorchInterval == 0)
            {
                Debug.LogWarning("Minimum torch interval is set to zero. Not spawning any torches.");
                return;
            }

            var size = _wallVisual.localScale.x;

            var torchCount = (size - _minTorchInterval) / _minTorchInterval;

            for (int i = 0; i < torchCount; i++)
            {
                var shouldSpawn = Random.Range(0, 100) <= _torchSpawnChance;
                if (shouldSpawn)
                {
                    var torch = Instantiate(_torchPrefab, transform);
                    torch.transform.rotation = transform.rotation;
                    torch.transform.localPosition = new Vector3((-size / 2f) + (_minTorchInterval / 2f) + (_minTorchInterval * i), 2.5f, 0.5f);
                    _torches.Add(torch);
                }
            }
        }
    }
}