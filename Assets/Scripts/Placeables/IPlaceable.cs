using System.Collections.Generic;

namespace GridSystem
{
    public interface IPlaceable
    {
        public List<GridCell> GridCells { get; set; }
        public void PlacedIn(GridCell occupiedGridCells)
        {
            if (!GridCells.Contains(occupiedGridCells))
                GridCells.Add(occupiedGridCells);
        }
    }
}