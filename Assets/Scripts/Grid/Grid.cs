using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public class Grid
    {
        public int Width { get; private set; }
        public int Length { get; private set; }
        public List<GridCell> Cells { get; private set; } = new();

        public Grid(int width, int length)
        {
            Width = width;
            Length = length;
            InitGrid();
        }

        private void InitGrid()
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Length; j++)
                {
                    GridCellType cellType;
                    if (i == 0 && j == 0)
                    {
                        cellType = GridCellType.BackLeftCorner;
                    }
                    else if (i == Width - 1 && j == 0)
                    {
                        cellType = GridCellType.FrontLeftCorner;
                    }
                    else if (i == 0 && j == Length - 1)
                    {
                        cellType = GridCellType.BackRightCorner;
                    }
                    else if (i == Width - 1 && j == Length - 1)
                    {
                        cellType = GridCellType.FrontRightCorner;
                    }
                    else if ((i == 0 || i == Width - 1) && j == Mathf.CeilToInt(Length / 2f) || i == Mathf.CeilToInt(Width / 2f) && (j == 0 || j == Length - 1))
                    {
                        cellType = GridCellType.Door;
                    }
                    else if ((i == 0 || i == Width - 1) && j == Mathf.CeilToInt(Length / 2f) + 1 || i == Mathf.CeilToInt(Width / 2f) + 1 && (j == 0 || j == Length - 1))
                    {
                        cellType = GridCellType.DoorSecondary;
                    }
                    else if (i == 0 || i == Width - 1 || j == 0 || j == Length - 1)
                    {
                        cellType = GridCellType.Edge;
                    }
                    else
                    {
                        cellType = GridCellType.Central;
                    }
                    var cellToAdd = new GridCell(i, j, new Vector3(-Width / 2f + i + 0.5f, 0f, -Length / 2f + j + 0.5f), cellType, this);

                    cellToAdd.SetupNeighbours();
                    Cells.Add(cellToAdd);
                }
            }
        }

        public GridCell GetCell(int x, int y)
        {
            return Cells[GetIndex(x, y)];
        }

        private int GetIndex(int x, int y)
        {
            return y + x * Length;
        }
    }
}