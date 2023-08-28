using GamePlay.EventSystem;
using GridSystem;
using GamePlay.RoomSystem.Creation;
using GamePlay.RoomSystem.Placeables.Doors;
using GamePlay.RoomSystem.Placeables;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Grid = GridSystem.Grid;
using GamePlay.AnimationSystem;
using System.Collections;

namespace GamePlay.RoomSystem.Rooms
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

        private List<Vector2> _possibleDirections = new() { new Vector2(-1, 0), new Vector2(1, 0), new Vector2(0, -1), new Vector2(0, 1) };
        private List<Vector2> _selectedDoorDirections = new();

        private List<WallData> _wallDatas = new();
        private List<GameObject> _walls = new();

        private void Awake()
        {
            EventManager.Instance.AddListener<DoorSelectedEvent>(DeActivateRoom);
            EventManager.Instance.AddListener<RoomSlidingEndedEvent>(AnimateRoomUnlock);
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
                cell.TryPlaceItem(generatedCornerPiece.GetComponent<IPlaceable>());
            }
            else if (cell.Type == GridCellType.BackLeftCorner)
            {
                var generatedCornerPiece = _prefabProvider.GetShortWallCorner(transform);
                generatedCornerPiece.transform.localPosition = cell.Position;
                cell.TryPlaceItem(generatedCornerPiece.GetComponent<IPlaceable>());
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
                var curCell = _grid.GetCell(0, i);

                if (wallData.StartCell == null && curCell.Type == GridCellType.Edge)
                {
                    wallData.StartCell = curCell;
                }

                if (i != startIndex && wallData.StartCell != null && curCell.Type != GridCellType.Edge)
                {
                    wallData.EndCell = _grid.GetCell(0, i - 1);

                    _wallDatas.Add(wallData);

                    if (i != _length - 1)
                    {
                        wallData = new();
                        wallData.Side = WallSide.Left;
                    }
                }
            }
        }

        private void SearchRightWall(int startIndex)
        {
            List<GridCell> visitedCells = new();

            WallData wallData = new();
            wallData.Side = WallSide.Right;

            for (int i = startIndex; i < _length; i++)
            {
                var curCell = _grid.GetCell(_width - 1, i);

                if (curCell.Type == GridCellType.Edge)
                    visitedCells.Add(curCell);

                if (wallData.StartCell == null && curCell.Type == GridCellType.Edge)
                {
                    wallData.StartCell = curCell;
                }

                if (i != startIndex && wallData.StartCell != null && curCell.Type != GridCellType.Edge)
                {
                    wallData.EndCell = _grid.GetCell(_width - 1, i - 1);
                    wallData.OccupiedCells = visitedCells;

                    _wallDatas.Add(wallData);

                    if (i != _length - 1)
                    {
                        wallData = new();
                        wallData.Side = WallSide.Right;
                    }
                }
            }
        }

        private void SearchFrontWall(int startIndex)
        {
            List<GridCell> visitedCells = new();

            WallData wallData = new();
            wallData.Side = WallSide.Front;
            for (int i = startIndex; i < _width; i++)
            {
                var curCell = _grid.GetCell(i, _length - 1);

                if (curCell.Type == GridCellType.Edge)
                    visitedCells.Add(curCell);

                if (wallData.StartCell == null && curCell.Type == GridCellType.Edge)
                {
                    wallData.StartCell = curCell;
                }

                if (i != startIndex && wallData.StartCell != null && curCell.Type != GridCellType.Edge)
                {
                    wallData.EndCell = _grid.GetCell(i - 1, _length - 1);
                    wallData.OccupiedCells = visitedCells;

                    _wallDatas.Add(wallData);

                    if (i != _width - 1)
                    {
                        wallData = new();
                        wallData.Side = WallSide.Front;
                    }
                }
            }
        }

        private void SearchBackWall(int startIndex)
        {
            List<GridCell> visitedCells = new();

            WallData wallData = new();
            wallData.Side = WallSide.Back;

            for (int i = startIndex; i < _width; i++)
            {
                var curCell = _grid.GetCell(i, 0);

                if (curCell.Type == GridCellType.Edge)
                    visitedCells.Add(curCell);

                if (wallData.StartCell == null && curCell.Type == GridCellType.Edge)
                {
                    wallData.StartCell = curCell;
                }

                if (i != startIndex && wallData.StartCell != null && curCell.Type != GridCellType.Edge)
                {
                    wallData.EndCell = _grid.GetCell(i - 1, 0);
                    wallData.OccupiedCells = visitedCells;

                    _wallDatas.Add(wallData);

                    if (i != _width - 1)
                    {
                        wallData = new();
                        wallData.Side = WallSide.Back;
                    }
                }
            }
        }

        private void GenerateWalls()
        {
            for (int i = 0; i < _wallDatas.Count; i++)
            {
                GameObject wall = null;
                var wallSize = _wallDatas[i].GetWallSize();
                var wallAngles = Vector3.zero;
                switch (_wallDatas[i].Side)
                {
                    case WallSide.Left:
                        wall = _prefabProvider.GetShortWall(transform);
                        wallAngles = new Vector3(0, 90, 0);

                        break;
                    case WallSide.Back:
                        wall = _prefabProvider.GetShortWall(transform);
                        wallAngles = new Vector3(0, 0, 0);
                        break;
                    case WallSide.Right:
                        wall = _prefabProvider.GetFullHeightWall(transform);
                        wallAngles = new Vector3(0, 270, 0);
                        break;
                    case WallSide.Front:
                        wall = _prefabProvider.GetFullHeightWall(transform);
                        wallAngles = new Vector3(0, 180, 0);
                        break;
                };

                if (_wallDatas[i].OccupiedCells != null)
                {
                    var placeable = wall.GetComponent<IPlaceable>();
                    for (int j = 0; j < _wallDatas[i].OccupiedCells.Count; j++)
                    {
                        _wallDatas[i].OccupiedCells[j].TryPlaceItem(placeable);
                    }
                }

                wall.transform.localScale = new Vector3(wallSize, 1, 1);
                wall.transform.eulerAngles = wallAngles;
                wall.transform.position = _wallDatas[i].GetCenter();
                wall.transform.parent = transform;
                wall.gameObject.name += _wallDatas[i].Side.ToString();
                _walls.Add(wall);
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
                    cell.ChangeCellType(GridCellType.Edge);
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

                    cell.TryPlaceItem(item);
                    Items.Add(item);
                }
            }
        }

        private void CreateDoorSecondaryPiece(GridCell cell)
        {
            if (cell.Type == GridCellType.DoorSecondary)
            {
                if (cell.Y == _length - 1 && !_selectedDoorDirections.Contains(new Vector2(0, 1)) || cell.X == _width - 1 && !_selectedDoorDirections.Contains(new Vector2(1, 0)) ||
                    cell.Y == 0 && !_selectedDoorDirections.Contains(new Vector2(0, -1)) || cell.X == 0 && !_selectedDoorDirections.Contains(new Vector2(-1, 0)))
                {
                    cell.ChangeCellType(GridCellType.Edge);
                }

                foreach (var neighbour in cell.Neighbours)
                {
                    if (neighbour.Value.Type == GridCellType.Door && neighbour.Value.Placeable != null)
                    {
                        cell.TryPlaceItem(neighbour.Value.Placeable);
                        break;
                    }
                }
            }
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

        public virtual void AnimateRoomUnlock(object eventData)
        {
            StartCoroutine(SpawnAnimationCo());
        }

        private IEnumerator SpawnAnimationCo()
        {
            foreach (var item in Items)
            {
                if (item is IAnimateable animateableItem)
                {
                    animateableItem.Animate();
                    yield return new WaitForSeconds(0.2f);
                }
            }

            EventManager.Instance.TriggerEvent<RoomSpawnAnimationFinishedEvent>();
        }

        private void OnDisable()
        {
            EventManager.Instance.RemoveListener<DoorSelectedEvent>(DeActivateRoom);
            EventManager.Instance.RemoveListener<RoomSlidingEndedEvent>(AnimateRoomUnlock);
        }
    }

    public struct WallData
    {
        public GridCell StartCell;
        public GridCell EndCell;
        public List<GridCell> OccupiedCells;

        public WallSide Side;

        public Vector3 GetCenter()
        {
            return (StartCell.Position + EndCell.Position) * 0.5f;
        }

        public float GetWallSize()
        {
            return Vector3.Distance(StartCell.Position, EndCell.Position) + 1;
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