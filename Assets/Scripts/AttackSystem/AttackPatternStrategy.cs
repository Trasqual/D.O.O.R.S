using UnityEngine;

public abstract class AttackPatternStrategy : ScriptableObject
{
    public abstract Vector3 GetPosition(int index, int count, Vector3 centerPos);
    public abstract Quaternion GetRotation(int index, int count, Vector3 centerPos);
}
