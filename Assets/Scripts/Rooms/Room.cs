using GridSystem;
using RoomSystem.Creation;
using RoomSystem.Doors;
using RoomSystem.Props;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Grid = GridSystem.Grid;

namespace RoomSystem.Rooms
{
    public class Room : MonoBehaviour
    {
        protected Grid _grid;
        private int _width;
        private int _length;
        private int _doorCount;
        private Vector2 _previousRoomDirection;
        private PropFactory _prefabProvider;
        public Vector2 Size => new Vector2(_width, _length);

        public List<IPlaceable> Items = new();
        public List<GameObject> Doors = new();
        public List<IPlaceable> Visuals = new();

        private List<Vector2> _possibleDirections = new() { new Vector2(-1, 0), new Vector2(1, 0), new Vector2(0, -1), new Vector2(0, 1) };
        private List<Vector2> _selectedDoorDirections = new();

        private List<WallData> _wallDatas = new();

        private void Awake()
        {
            EventManager.Instance.AddListener<DoorSelectedEvent>(DeActivateRoom);
        }

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
                CreateDoorPiece(cell);
                CreateDoorSecondaryPiece(cell);
            }

            CalculateWallDatas();
        }

        private void CreateCornerPiece(GridCell cell)
        {
            if (cell.Type == GridCellType.FrontRightCorner || cell.Type == GridCellType.FrontLeftCorner || cell.Type == GridCellType.BackRightCorner)
            {
                var generatedCornerPiece = _prefabProvider.GetFullHeightWallCorner(transform);
                generatedCornerPiece.transform.localPosition = cell.Position;
                //cell.PlaceItem(generatedCornerPiece.GetComponent<IPlaceable>());
            }
            else if (cell.Type == GridCellType.BackLeftCorner)
            {
                var generatedCornerPiece = _prefabProvider.GetShortWallCorner(transform);
                generatedCornerPiece.transform.localPosition = cell.Position;
                //cell.PlaceItem(generatedCornerPiece.GetComponent<IPlaceable>());
            }
        }

        private void CalculateWallDatas()
        {
            SearchLeftWall(0);
            SearchRightWall(0);
            SearchFrontWall(0);
            SearchBackWall(0);

            GenerateWalls();
        }

        private void SearchLeftWall(int startIndex)
        {
            WallData wallData = new();
            wallData.Side = WallSide.Left;
            for (int i = startIndex; i < _length; i++)
            {
                var curCell = _grid.Cells[GetIndex(0, i, _length)];

                if (wallData.StartCell == null && curCell.Type == GridCellType.Edge)
                {
                    wallData.StartCell = curCell;
                }

                if (i != startIndex && curCell.Type != GridCellType.Edge)
                {
                    wallData.EndCell = _grid.Cells[GetIndex(0, i - 1, _length)];
                    _wallDatas.Add(wallData);
                    //if (i != _length - 1)
                    //SearchLeftWall(i - 1);
                    break;
                }
            }
        }

        private void SearchRightWall(int startIndex)
        {
            WallData wallData = new();
            wallData.Side = WallSide.Right;

            for (int i = startIndex; i < _length; i++)
            {
                var curCell = _grid.Cells[GetIndex(_width - 1, i, _length)];

                if (wallData.StartCell == null && curCell.Type == GridCellType.Edge)
                {
                    wallData.StartCell = curCell;
                }

                if (i != startIndex && curCell.Type != GridCellType.Edge)
                {
                    wallData.EndCell = _grid.Cells[GetIndex(_width - 1, i - 1, _length)];
                    _wallDatas.Add(wallData);

                    //if (i != _length - 1)
                    //SearchRightWall(i - 1);
                    break;
                }
            }
        }

        private void SearchFrontWall(int startIndex)
        {
            WallData wallData = new();
            wallData.Side = WallSide.Front;
            for (int i = startIndex; i < _width; i++)
            {
                var curCell = _grid.Cells[GetIndex(i, _length - 1, _width)];

                if (wallData.StartCell == null && curCell.Type == GridCellType.Edge)
                {
                    wallData.StartCell = curCell;
                }

                if (i != startIndex && curCell.Type != GridCellType.Edge)
                {
                    wallData.EndCell = _grid.Cells[GetIndex(i - 1, _length - 1, _width)];
                    _wallDatas.Add(wallData);

                    //if (i != _width - 1)
                    //SearchFrontWall(i - 1);
                    break;
                }
            }
        }

        private void SearchBackWall(int startIndex)
        {
            WallData wallData = new();
            wallData.Side = WallSide.Back;

            for (int i = startIndex; i < _width; i++)
            {
                var curCell = _grid.Cells[GetIndex(i, 0, _width)];

                if (wallData.StartCell == null && curCell.Type == GridCellType.Edge)
                {
                    wallData.StartCell = curCell;
                }

                if (i != startIndex && curCell.Type != GridCellType.Edge)
                {
                    wallData.EndCell = _grid.Cells[GetIndex(i - 1, 0, _width)];
                    _wallDatas.Add(wallData);

                    //if (i != _width - 1)
                    //SearchBackWall(i - 1);
                    break;
                }
            }
        }

        private int GetIndex(int x, int y, int size)
        {
            return x + y * size;
        }

        private void GenerateWalls()
        {
            for (int i = 0; i < _wallDatas.Count; i++)
            {
                GameObject wall = null;
                var wallSize = _wallDatas[i].GetWallSize();
                switch (_wallDatas[i].Side)
                {
                    case WallSide.Left:
                        wall = _prefabProvider.GetShortWall(transform);
                        wall.transform.eulerAngles = new Vector3(0, 90, 0);
                        break;
                    case WallSide.Back:
                        wall = _prefabProvider.GetShortWall(transform);
                        wall.transform.eulerAngles = new Vector3(0, 0, 0);
                        break;
                    case WallSide.Right:
                        wall = _prefabProvider.GetFullHeightWall(transform);
                        wall.transform.eulerAngles = new Vector3(0, 270, 0);
                        break;
                    case WallSide.Front:
                        wall = _prefabProvider.GetFullHeightWall(transform);
                        wall.transform.eulerAngles = new Vector3(0, 180, 0);
                        break;
                };
                wall.transform.localScale = new Vector3(wallSize, 1, 1);
            }
        }

        private void CreateDoorPiece(GridCell cell)
        {
            if (cell.Type == GridCellType.Door)
            {
                GameObject generatedPiece = null;

                Vector2 doorSide = Vector2.zero;
                if (cell.Y == _length - 1 && _selectedDoorDirections.Contains(new Vector2(0, 1)))
                {
                    generatedPiece = _prefabProvider.GetFullHeightDoor(transform);
                    doorSide = new Vector2(0, 1);
                }
                else if (cell.X == _width - 1 && _selectedDoorDirections.Contains(new Vector2(1, 0)))
                {
                    generatedPiece = _prefabProvider.GetFullHeightDoor(transform);
                    doorSide = new Vector2(1, 0);
                }
                else if (cell.Y == 0 && _selectedDoorDirections.Contains(new Vector2(0, -1)))
                {
                    generatedPiece = _prefabProvider.GetShortDoor(transform);
                    doorSide = new Vector2(0, -1);
                }
                else if (cell.X == 0 && _selectedDoorDirections.Contains(new Vector2(-1, 0)))
                {
                    generatedPiece = _prefabProvider.GetShortDoor(transform);
                    doorSide = new Vector2(-1, 0);
                }
                else
                {
                    ChangeCellType(cell);
                }
                if (generatedPiece != null)
                {
                    var door = generatedPiece.GetComponent<Door>();
                    bool doorIsActive = true;
                    if (_previousRoomDirection != Vector2.zero && doorSide == -_previousRoomDirection)
                    {
                        doorIsActive = false;
                    }
                    door.Init(doorSide, RoomType.Creature, this, doorIsActive);

                    Doors.Add(generatedPiece);

                    var rot = generatedPiece.transform.eulerAngles;
                    if (cell.X == 0)
                    {
                        rot.y = 90;
                        generatedPiece.transform.localPosition = cell.Position + new Vector3(0f, 0f, 0.5f);
                    }
                    else if (cell.X == _width - 1)
                    {
                        rot.y = 270;
                        generatedPiece.transform.localPosition = cell.Position + new Vector3(0f, 0f, 0.5f); ;
                    }
                    else if (cell.Y == 0)
                    {
                        rot.y = 0;
                        generatedPiece.transform.localPosition = cell.Position + new Vector3(0.5f, 0f, 0f);
                    }
                    else if (cell.Y == _length - 1)
                    {
                        rot.y = 180;
                        generatedPiece.transform.localPosition = cell.Position + new Vector3(0.5f, 0f, 0f);
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

                if (cell.Y == _length - 1 && !_selectedDoorDirections.Contains(new Vector2(0, 1)) || cell.X == _width - 1 && !_selectedDoorDirections.Contains(new Vector2(1, 0)) ||
                    cell.Y == 0 && !_selectedDoorDirections.Contains(new Vector2(0, -1)) || cell.X == 0 && !_selectedDoorDirections.Contains(new Vector2(-1, 0)))
                {
                    ChangeCellType(cell);
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
                    generatedPiece.transform.localPosition = cell.Position;
                    IPlaceable item = generatedPiece.GetComponent<IPlaceable>();

                    //cell.PlaceItem(item);
                    Visuals.Add(item);
                }
            }
        }

        private void ChangeCellType(GridCell cell)
        {
            cell.ChangeCellType(GridCellType.Edge);
        }

        public void DeActivateRoom(object data)
        {
            var room = ((DoorSelectedEvent)data).DoorData.Room;
            if (room == this)
                gameObject.SetActive(false);
        }

        private void GenerateProps()
        {

        }

        public virtual void AnimateRoomUnlock()
        {

        }

        private void OnDisable()
        {
            EventManager.Instance.RemoveListener<DoorSelectedEvent>(DeActivateRoom);
        }
    }

    public struct WallData
    {
        public GridCell StartCell;
        public GridCell EndCell;

        public WallSide Side;

        public float GetWallSize()
        {
            return Vector3.Distance(StartCell.Position, EndCell.Position);
        }
    }

    public enum WallSide
    {
        Left,
        Right,
        Front,
        Back
    }
}