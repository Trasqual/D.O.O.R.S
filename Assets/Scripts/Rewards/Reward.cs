using UnityEngine;

namespace GamePlay.Rewards
{
    public abstract class Reward : ScriptableObject
    {
        public abstract void GiveReward();
    }
}