using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace GamePlay.CommandSystem
{
    public class MovePlayerToNewRoomCommand : ICommand
    {
        public RoomCreationPriority Priority { get; set; }
        private readonly Transform _player;
        private readonly Vector3 _moveAmount;
        private Action _onComplete;

        public MovePlayerToNewRoomCommand(Transform player, Vector3 moveAmount, Action OnComplete, RoomCreationPriority priority)
        {
            _player = player;
            _moveAmount = moveAmount;
            _onComplete = OnComplete;
            Priority = priority;
        }


        public void Execute(Action OnComplete)
        {
            if (OnComplete != null && _onComplete != null)
                OnComplete += _onComplete;

            if (NavMesh.SamplePosition(_player.position, out NavMeshHit hit, 100, 1))
            {
                _player.DOMove(hit.position - _moveAmount.normalized * 2f, 1f).OnComplete(() =>
                {
                    OnComplete?.Invoke();
                });
            }
        }
    }
}