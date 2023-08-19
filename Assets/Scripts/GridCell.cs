using UnityEngine;

public class GridCell
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public Vector3 Position => new(X, Y, 0);
    public GridCellType Type { get; private set; }

    public GridCell(int x, int y, GridCellType cellType)
    {
        X = x;
        Y = y;
        Type = cellType;
    }

    public void PlaceItem(IPlaceable placeable)
    {
        placeable.PlacedIn(this);
    }
}
