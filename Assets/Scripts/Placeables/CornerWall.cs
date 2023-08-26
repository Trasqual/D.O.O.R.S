using GridSystem;
using UnityEngine;
using System.Collections.Generic;

namespace GamePlay.RoomSystem.Placeables
{
    public class CornerWall : MonoBehaviour, IPlaceable
    {
        public List<GridCell> GridCells { get; set; } = new();
    }
}