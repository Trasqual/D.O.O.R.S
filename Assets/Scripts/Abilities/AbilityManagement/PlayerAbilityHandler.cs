using GamePlay.EventSystem;
using GamePlay.Rewards.AbilityRewards;

namespace GamePlay.Abilities.Management
{
    public class PlayerAbilityHandler : AbilityHandlerBase
    {
        private void Awake()
        {
            EventManager.Instance.AddListener<AbilityReward>(GainAbility);
            EventManager.Instance.AddListener<DoorSelectedEvent>(StopAbilities);
            EventManager.Instance.AddListener<CharacterEnteredNewRoomEvent>(StartAbilities);
        }

        private void OnDisable()
        {
            EventManager.Instance.RemoveListener<AbilityReward>(GainAbility);
            EventManager.Instance.RemoveListener<DoorSelectedEvent>(StopAbilities);
            EventManager.Instance.RemoveListener<CharacterEnteredNewRoomEvent>(StartAbilities);
        }

        private void Update()
        {
            UpdateAbilities();
        }
    }
}