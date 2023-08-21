using UnityEngine;

namespace GridSystem
{
    public class GridCell
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public Vector3 Position;
        public GridCellType Type { get; private set; }

        public GridCell(int x, int y, Vector3 position, GridCellType cellType)
        {
            X = x;
            Y = y;
            Position = position;
            Type = cellType;
        }

        public void ChangeCellType(GridCellType cellType)
        {
            Type = cellType;
        }

        public void PlaceItem(IPlaceable placeable)
        {
            placeable.PlacedIn(this);
        }
    }
}