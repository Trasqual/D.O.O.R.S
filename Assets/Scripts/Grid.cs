
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
                if (i == 0 && j == 0 || i == _width - 1 && j == 0 || i == 0 && j == _length - 1 || i == _width - 1 && j == _length - 1)
                {
                    cellType = GridCellType.Corner;
                }
                else if (i == 0 || j == 0 || i == _width - 1 || j == _length - 1)
                {
                    cellType = GridCellType.Edge;
                }
                else
                {
                    cellType = GridCellType.Central;
                }

                Cells.Add(new GridCell(Mathf.CeilToInt(-_width / 2f + i), Mathf.CeilToInt(-_length / 2f + j), cellType));
            }
        }
    }
}
