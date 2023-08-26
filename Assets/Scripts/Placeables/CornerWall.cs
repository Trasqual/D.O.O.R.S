using GridSystem;
using UnityEngine;
using System.Collections.Generic;

public class CornerWall : MonoBehaviour, IPlaceable
{
    public List<GridCell> GridCells { get; set; } = new();
}
