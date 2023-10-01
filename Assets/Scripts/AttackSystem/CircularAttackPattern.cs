using UnityEngine;

public class CircularAttackPattern : AttackPatternStrategy
{
    [SerializeField] private float _radius;

    public override Vector3 GetPosition(int index, int count, Vector3 centerPos)
    {
        var x = centerPos.x + _radius * Mathf.Cos((index * 360 / count) * Mathf.Deg2Rad);
        var z = centerPos.z + _radius * Mathf.Cos((index * 360 / count) * Mathf.Deg2Rad);

        return new Vector3(x, 0f, z);
    }

    public override Quaternion GetRotation(int index, int count, Vector3 centerPos)
    {
        return Quaternion.LookRotation(GetPosition(index, count, centerPos) - centerPos);
    }
}
