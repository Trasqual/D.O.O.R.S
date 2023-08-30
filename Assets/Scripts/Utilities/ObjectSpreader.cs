using UnityEngine;

public class ObjectSpreader : MonoBehaviour
{
    public static float[] GetLineSpreadPosition(float size, int count)
    {
        var positions = new float[count];

        for (int i = 0; i < count; i++)
        {
            positions[i] = (-size * 0.5f) + (size / (count - 1)) * i;
        }

        return positions;
    }
}
