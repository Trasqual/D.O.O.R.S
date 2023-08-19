using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    protected Grid _grid;
    private int _width;
    private int _length;

    public List<IPlaceable> Items = new();
    public List<IPlaceable> Visuals = new();

    public void Init(int width, int length)
    {
        _width = width;
        _length = length;
        _grid = new Grid(width, length);
    }

    public virtual void FillRoom()
    {
        GenerateFloor();
        GenerateWalls();
        GenerateProps();
    }

    private void GenerateFloor()
    {
        var floor = PrefabProvider.Instance.GetFloor(transform);
        floor.transform.localScale = new Vector3(_width, _length);
    }

    private void GenerateWalls()
    {
        for (int i = 0; i < _grid.Cells.Count; i++)
        {
            var cell = _grid.Cells[i];
            if (cell.Type == GridCellType.Corner)
            {
                var generatedCornerPiece = PrefabProvider.Instance.GetWallCorner(transform);
                cell.PlaceItem(generatedCornerPiece.GetComponent<IPlaceable>());
            }
            else if (cell.Type == GridCellType.Edge)
            {
                var generatedEdgePiece = PrefabProvider.Instance.GetWall(transform);

                var rot = generatedEdgePiece.transform.localRotation;
                if (cell.X == 0)
                {
                    rot.y = 90;
                }
                else if (cell.X == _width - 1)
                {
                    rot.y = 270;
                }
                else if (cell.Y == 0)
                {
                    rot.y = 0;
                }
                else if (cell.Y == _length - 1)
                {
                    rot.y = 180;
                }
                generatedEdgePiece.transform.localRotation = rot;

                IPlaceable item = generatedEdgePiece.GetComponent<IPlaceable>();

                cell.PlaceItem(item);
                Visuals.Add(item);
            }
        }
    }

    private void GenerateProps()
    {

    }

    public virtual void AnimateRoomUnlock()
    {

    }
}
