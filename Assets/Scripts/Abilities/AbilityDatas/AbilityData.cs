using GamePlay.Abilities.Attacks;
using UnityEngine;

[CreateAssetMenu(menuName = "AbilityData")]
public class AbilityData : ScriptableObject
{
    public string Name;
    public AbilityBase Prefab;
}
