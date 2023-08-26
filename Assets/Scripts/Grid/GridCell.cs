using GridSystem.Directions;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public class GridCell
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public Vector3 Position;
        public GridCellType Type { get; private set; }
        public Grid Grid { get; private set; }
        public IPlaceable Placeable { get; private set; }
        public Dictionary<DirectionType, GridCell> Neighbours = new();

        public GridCell(int x, int y, Vector3 position, GridCellType cellType, Grid grid)
        {
            X = x;
            Y = y;
            Position = position;
            Type = cellType;
            Grid = grid;
        }

        public void ChangeCellType(GridCellType cellType)
        {
            Type = cellType;
        }

        public void AddNeighbour(DirectionType direction, GridCell cell)
        {
            Neighbours[direction] = cell;
        }

        public void SetupNeighbours()
        {
            Neighbours.Clear();

            if (X > 0 && X <= Grid.Width - 1)
            {
                AddNeighbour(DirectionType.Left, Grid.GetCell(X - 1, Y));
                Neighbours[DirectionType.Left].AddNeighbour(DirectionType.Right, this);
            }

            if (Y > 0 && Y <= Grid.Length - 1)
            {
                AddNeighbour(DirectionType.Down, Grid.GetCell(X, Y - 1));
                Neighbours[DirectionType.Down].AddNeighbour(DirectionType.Up, this);
            }
        }

        public bool TryPlaceItem(IPlaceable placeable)
        {
            if (Placeable == null)
            {
                Placeable = placeable;
                Placeable.PlacedIn(this);
                return true;
            }
            return false;
        }

        public void ClearItem()
        {
            Placeable = null;
        }
    }
}