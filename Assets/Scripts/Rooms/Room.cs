using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Room : MonoBehaviour
{
    protected Grid _grid;
    private int _width;
    private int _length;
    private int _doorCount;
    private Vector2 _previousRoomDirection;
    private PropFactory _prefabProvider;

    public List<IPlaceable> Items = new();
    public List<GameObject> Doors = new();
    public List<IPlaceable> Visuals = new();

    private List<Vector2> _possibleDirections = new() { new Vector2(-1, 0), new Vector2(1, 0), new Vector2(0, -1), new Vector2(0, 1) };
    private List<Vector2> _selectedDoorDirections = new();

    public void Init(int width, int length, int doorCount, Vector2 previousRoomDirection, PropFactory prefabProvider)
    {
        _width = width;
        _length = length;
        _doorCount = doorCount;
        _previousRoomDirection = previousRoomDirection;
        _prefabProvider = prefabProvider;
        _grid = new Grid(width, length);
        GetDoorDirections();
        FillRoom();
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    private void GetDoorDirections()
    {
        if (_previousRoomDirection != Vector2.zero)
        {
            _selectedDoorDirections.Add(_previousRoomDirection * -1);
            _possibleDirections.Remove(_previousRoomDirection * -1);

            for (int i = 1; i < _doorCount; i++)
            {
                var rand = Random.Range(0, _possibleDirections.Count);

                _selectedDoorDirections.Add(_possibleDirections[rand]);
                _possibleDirections.RemoveAt(rand);
            }
        }
        else
        {
            for (int i = 0; i < _doorCount; i++)
            {
                var rand = Random.Range(0, _possibleDirections.Count);

                _selectedDoorDirections.Add(_possibleDirections[rand]);
                _possibleDirections.RemoveAt(rand);
            }
        }
    }

    protected virtual void FillRoom()
    {
        GenerateFloor();
        GenerateWallsAndDoors();
        GenerateProps();
    }

    private void GenerateFloor()
    {
        var floor = _prefabProvider.GetFloor(transform);
        floor.transform.localScale = new Vector3(_width, 1f, _length);
    }

    private void GenerateWallsAndDoors()
    {
        for (int i = 0; i < _grid.Cells.Count; i++)
        {
            var cell = _grid.Cells[i];

            CreateCornerPiece(cell);
            CreateWallPiece(cell);
            CreateDoorPiece(cell);
            CreateDoorSecondaryPiece(cell);
        }
    }

    private void CreateCornerPiece(GridCell cell)
    {
        if (cell.Type == GridCellType.FrontRightCorner || cell.Type == GridCellType.FrontLeftCorner || cell.Type == GridCellType.BackRightCorner)
        {
            var generatedCornerPiece = _prefabProvider.GetFullHeightWallCorner(transform);
            generatedCornerPiece.transform.position = cell.Position;
            //cell.PlaceItem(generatedCornerPiece.GetComponent<IPlaceable>());
        }
        else if (cell.Type == GridCellType.BackLeftCorner)
        {
            var generatedCornerPiece = _prefabProvider.GetShortWallCorner(transform);
            generatedCornerPiece.transform.position = cell.Position;
            //cell.PlaceItem(generatedCornerPiece.GetComponent<IPlaceable>());
        }
    }

    private void CreateWallPiece(GridCell cell)
    {
        if (cell.Type == GridCellType.FrontEdge || cell.Type == GridCellType.RightEdge)
        {
            var generatedEdgePiece = _prefabProvider.GetFullHeightWall(transform);

            var rot = generatedEdgePiece.transform.eulerAngles;
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
            generatedEdgePiece.transform.eulerAngles = rot;
            generatedEdgePiece.transform.position = cell.Position;
            IPlaceable item = generatedEdgePiece.GetComponent<IPlaceable>();

            //cell.PlaceItem(item);
            Visuals.Add(item);
        }
        else if (cell.Type == GridCellType.BackEdge || cell.Type == GridCellType.LeftEdge)
        {
            var generatedEdgePiece = _prefabProvider.GetShortWall(transform);

            var rot = generatedEdgePiece.transform.eulerAngles;
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
            generatedEdgePiece.transform.eulerAngles = rot;
            generatedEdgePiece.transform.position = cell.Position;
            IPlaceable item = generatedEdgePiece.GetComponent<IPlaceable>();

            //cell.PlaceItem(item);
            Visuals.Add(item);

        }
    }

    private void CreateDoorPiece(GridCell cell)
    {
        if (cell.Type == GridCellType.Door)
        {
            GameObject generatedPiece = null;

            if ((cell.Y == _length - 1 && _selectedDoorDirections.Contains(new Vector2(0, 1))) || (cell.X == _width - 1 && _selectedDoorDirections.Contains(new Vector2(1, 0))))
            {
                generatedPiece = _prefabProvider.GetFullHeightDoor(transform);
            }
            else if ((cell.Y == 0 && _selectedDoorDirections.Contains(new Vector2(0, -1))) || (cell.X == 0 && _selectedDoorDirections.Contains(new Vector2(-1, 0))))
            {
                generatedPiece = _prefabProvider.GetShortDoor(transform);
            }
            else
            {
                ChangeCellType(cell);
                CreateWallPiece(cell);
            }
            if (generatedPiece != null)
            {
                var rot = generatedPiece.transform.eulerAngles;
                if (cell.X == 0)
                {
                    rot.y = 90;
                    generatedPiece.transform.position = cell.Position + new Vector3(0f, 0f, 0.5f);
                }
                else if (cell.X == _width - 1)
                {
                    rot.y = 270;
                    generatedPiece.transform.position = cell.Position + new Vector3(0f, 0f, 0.5f); ;
                }
                else if (cell.Y == 0)
                {
                    rot.y = 0;
                    generatedPiece.transform.position = cell.Position + new Vector3(0.5f, 0f, 0f);
                }
                else if (cell.Y == _length - 1)
                {
                    rot.y = 180;
                    generatedPiece.transform.position = cell.Position + new Vector3(0.5f, 0f, 0f);
                }
                generatedPiece.transform.eulerAngles = rot;
                IPlaceable item = generatedPiece.GetComponent<IPlaceable>();

                //cell.PlaceItem(item);
                Visuals.Add(item);
            }
        }
    }

    private void CreateDoorSecondaryPiece(GridCell cell)
    {
        if (cell.Type == GridCellType.DoorSecondary)
        {
            GameObject generatedPiece = null;

            if ((cell.Y == _length - 1 && !_selectedDoorDirections.Contains(new Vector2(0, 1))) || (cell.X == _width - 1 && !_selectedDoorDirections.Contains(new Vector2(1, 0))) ||
                (cell.Y == 0 && !_selectedDoorDirections.Contains(new Vector2(0, -1))) || (cell.X == 0 && !_selectedDoorDirections.Contains(new Vector2(-1, 0))))
            {
                ChangeCellType(cell);
                CreateWallPiece(cell);
            }

            if (generatedPiece != null)
            {
                var rot = generatedPiece.transform.eulerAngles;
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
                generatedPiece.transform.eulerAngles = rot;
                generatedPiece.transform.position = cell.Position;
                IPlaceable item = generatedPiece.GetComponent<IPlaceable>();

                //cell.PlaceItem(item);
                Visuals.Add(item);
            }
        }
    }

    private void ChangeCellType(GridCell cell)
    {
        if (cell.X == 0)
        {
            cell.ChangeCellType(GridCellType.LeftEdge);
        }
        else if (cell.X == _width - 1)
        {
            cell.ChangeCellType(GridCellType.RightEdge);
        }
        else if (cell.Y == 0)
        {
            cell.ChangeCellType(GridCellType.BackEdge);
        }
        else if (cell.Y == _length - 1)
        {
            cell.ChangeCellType(GridCellType.FrontEdge);
        }
    }

    private void GenerateProps()
    {

    }

    public virtual void AnimateRoomUnlock()
    {

    }
}
