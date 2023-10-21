using GamePlay.EnemySystem;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    private List<Enemy> _enemies = new();
    public int EnemyCount => _enemies.Count;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            if (!_enemies.Contains(enemy))
            {
                _enemies.Add(enemy);
                enemy.OnDeath += RemoveEnemy;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            if (_enemies.Contains(enemy))
                RemoveEnemy(enemy);
        }
    }

    private void RemoveEnemy(Enemy enemy)
    {
        enemy.OnDeath -= RemoveEnemy;
        _enemies.Remove(enemy);
    }


    public Enemy GetClosestEnemy()
    {
        var closestDist = float.MaxValue;
        Enemy closestEnemy = null;

        for (int i = 0; i < _enemies.Count; i++)
        {
            var sqrMgt = (transform.position - _enemies[0].transform.position).sqrMagnitude;
            if (sqrMgt < closestDist)
            {
                closestDist = sqrMgt;
                closestEnemy = _enemies[i];
            }
        }

        return closestEnemy;
    }
}
