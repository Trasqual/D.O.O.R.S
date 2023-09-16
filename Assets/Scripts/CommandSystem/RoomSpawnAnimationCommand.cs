using DG.Tweening;
using GamePlay.AnimationSystem;
using GridSystem;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GamePlay.CommandSystem
{
    public class RoomSpawnAnimationCommand : ICommand
    {
        public RoomCreationPriority Priority { get; set; }
        private readonly List<IPlaceable> _items;
        private readonly Action _onReadyForNavmesh;

        public RoomSpawnAnimationCommand(List<IPlaceable> items, Action onReadyForNavmesh, RoomCreationPriority priority)
        {
            _items = items;
            _onReadyForNavmesh = onReadyForNavmesh;
            Priority = priority;
        }

        public async void Execute(Action OnComplete)
        {
            foreach (var item in _items)
            {
                if (item is IAnimateable animateableItem)
                {
                    animateableItem.Animate(null);
                    await Task.Delay(700);
                }
            }
            DOVirtual.DelayedCall(0.5f, () =>
            {
                _onReadyForNavmesh?.Invoke();
            });

            await Task.Delay(1500);
            OnComplete?.Invoke();
        }
    }
}