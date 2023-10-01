using GamePlay.StatSystem;
using GamePlay.Visuals;
using UnityEngine;

public abstract class AttackBase : ScriptableObject
{
    public Visual _attackVisualPrefab;
    public StatController _statController;

    public virtual void Initialize(StatController statController)
    {
        _statController = statController;
        _attackVisualPrefab = _statController.GetStat<VisualStat>().Prefab;
    }

    public abstract void Perform();
}
