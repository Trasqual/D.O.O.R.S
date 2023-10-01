using GamePlay.StatSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatController : MonoBehaviour
{
    public Action OnInitialized;
    [SerializeReference] public List<StatBase> Stats = new();

    private void Start()
    {
        for (int i = 0; i < Stats.Count; i++)
        {
            Stats[i].Init();
            Stats[i].SubscribeToUpgrade();
        }
        OnInitialized?.Invoke();
    }

    private void OnDisable()
    {
        for (int i = 0; i < Stats.Count; i++)
        {
            Stats[i].UnsubscribeToUpgrade();
        }
    }

    public T GetStat<T>() where T : StatBase
    {
        return Stats.FirstOrDefault((x) => x.GetType() == typeof(T)) as T;
    }

    [ContextMenu(nameof(AddDamageStat))]
    public void AddDamageStat()
    {
        Stats.Add(new DamageStat("Damage"));
    }

    [ContextMenu(nameof(AddFireRateStat))]
    public void AddFireRateStat()
    {
        Stats.Add(new FireRateStat("FireRate"));
    }

    [ContextMenu(nameof(AddRangeStat))]
    public void AddRangeStat()
    {
        Stats.Add(new RangeStat("Range"));
    }

    [ContextMenu(nameof(AddHealthStat))]
    public void AddHealthStat()
    {
        Stats.Add(new HealthStat("Health"));
    }

    [ContextMenu(nameof(AddArmorStat))]
    public void AddArmorStat()
    {
        Stats.Add(new ArmorStat("Armor"));
    }

    [ContextMenu(nameof(AddProjectileStat))]
    public void AddProjectileStat()
    {
        Stats.Add(new VisualStat("Projectile"));
    }

    [ContextMenu(nameof(AddAreaStat))]
    public void AddAreaStat()
    {
        Stats.Add(new AreaStat("Area"));
    }

    [ContextMenu(nameof(AddProjectileCountStat))]
    public void AddProjectileCountStat()
    {
        Stats.Add(new ProjectileCountStat("ProjectileCount"));
    }
}
