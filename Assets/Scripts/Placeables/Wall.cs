using GridSystem;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour, IPlaceable
{
    public List<GridCell> GridCells { get; set; } = new();
}
