using UnityEngine;

public class DirectAttackPattern : AttackPatternStrategy
{
    public override Vector3 GetPosition(int index, int count, Vector3 centerPos)
    {
        return Vector3.zero;
    }

    public override Quaternion GetRotation(int index, int count, Vector3 centerPos)
    {
        return Quaternion.identity;
    }
}
