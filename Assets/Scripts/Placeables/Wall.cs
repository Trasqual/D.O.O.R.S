using GridSystem;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.RoomSystem.Placeables
{
    public class Wall : MonoBehaviour, IPlaceable
    {
        public List<GridCell> GridCells { get; set; } = new();
    }
}