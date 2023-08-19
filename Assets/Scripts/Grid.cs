
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private readonly int _width;
    private readonly int _length;
    public List<GridCell> Cells { get; private set; } = new();

    public Grid(int width, int length)
    {
        _width = width;
        _length = length;
        InitGrid();
    }

    private void InitGrid()
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _length; j++)
            {
                GridCellType cellType;
                if (i == 0 && j == 0)
                {
                    cellType = GridCellType.BackLeftCorner;
                }
                else if (i == _width - 1 && j == 0)
                {
                    cellType = GridCellType.FrontLeftCorner;
                }
                else if (i == 0 && j == _length - 1)
                {
                    cellType = GridCellType.BackRightCorner;
                }
                else if (i == _width - 1 && j == _length - 1)
                {
                    cellType = GridCellType.FrontRightCorner;
                }
                else if (((i == 0 || i == _width - 1) && j == Mathf.CeilToInt(_length / 2f)) || (i == Mathf.CeilToInt(_width / 2f) && (j == 0 || j == _length - 1)))
                {
                    cellType = GridCellType.Door;
                }
                else if (((i == 0 || i == _width - 1) && j == Mathf.CeilToInt(_length / 2f) + 1) || (i == Mathf.CeilToInt(_width / 2f) + 1 && (j == 0 || j == _length - 1)))
                {
                    cellType = GridCellType.DoorSecondary;
                }
                else if (i == 0)
                {
                    cellType = GridCellType.BackEdge;
                }
                else if (i == _width - 1)
                {
                    cellType = GridCellType.FrontEdge;
                }
                else if (j == 0)
                {
                    cellType = GridCellType.LeftEdge;
                }
                else if (j == _length - 1)
                {
                    cellType = GridCellType.RightEdge;
                }
                else
                {
                    cellType = GridCellType.Central;
                }

                Cells.Add(new GridCell(i, j, new Vector3(Mathf.CeilToInt(-_width / 2f + i), 0f, Mathf.CeilToInt(-_length / 2f + j)), cellType));
            }
        }
    }
}
