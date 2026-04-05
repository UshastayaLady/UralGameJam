using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;

    [Header("Grid Settings")]
    [SerializeField] private float _cellSize = 1;
    [SerializeField] private int _width = 5;
    [SerializeField] private int _height = 5;
    [SerializeField] private Transform _gridOrigin;
    private Slot[,] _grid;

    public void Initialize()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
            Instance = this;
        }

        CreateGrid();

    }

    private void CreateGrid()
    {
        _grid = new Slot[_width, _height];
        var id = 1;

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                //Vector2Int gridPos = new Vector2Int(x, y);
                //Vector3 worldPos = GridToWorld(gridPos);
                _grid[x,y] = new Slot();
                _grid[x, y].GridPos = new Vector2Int(x,y);
                _grid[x, y].WorldPos = GridToWorld(_grid[x, y].GridPos);
                _grid[x, y].CorrectID = id;
                id++;
                if (x == Mathf.CeilToInt(_width / 2) && y == Mathf.CeilToInt(_height / 2))
                {
                    _grid[x, y].IsEmpty = false;
                }
            }
        }
    }

    public Vector2 GridToWorld(Vector2Int gridPos)
    {
        // Сначала получаем левый нижний угол
        float leftBottomX = _gridOrigin.position.x + (gridPos.x * _cellSize);
        float leftBottomY = _gridOrigin.position.y + (gridPos.y * _cellSize);

        // Добавляем пол-клетки чтобы получить центр
        float centerX = leftBottomX + (_cellSize / 2f);
        float centerY = leftBottomY + (_cellSize / 2f);

        return new Vector2(centerX, centerY);
    }

    public Vector2Int WorldToGrid(Vector2 worldPos)
    {
        // Сдвигаем мировую позицию относительно левого нижнего угла
        float offsetX = worldPos.x - _gridOrigin.position.x;
        float offsetY = worldPos.y - _gridOrigin.position.y;

        // Вычитаем пол-клетки потому что worldPos может быть в центре
        float gridX = (offsetX - (_cellSize / 2f)) / _cellSize;
        float gridY = (offsetY - (_cellSize / 2f)) / _cellSize;

        // Округляем до ближайшей клетки
        int col = Mathf.RoundToInt(gridX);
        int row = Mathf.RoundToInt(gridY);

        return new Vector2Int(col, row);
    }

    private void Awake()
    {
        Initialize();
    }

    public Slot CheckPuzzleOnGrid(Bounds bounds)
    {
        Slot bestSlot = null;
        float maxOverlapArea = 0f;

        for (int i = 0; i < _width;i++)
        {
            for(int j = 0; j < _height; j++)
            {
                var cellBounds = new Bounds(_grid[i,j].WorldPos, new Vector2(_cellSize, _cellSize));

                if (bounds.Intersects(cellBounds) && _grid[i, j].IsEmpty)
                {
                    float overlapArea = GetIntersectionArea(bounds, cellBounds);

                    if (overlapArea > maxOverlapArea)
                    {
                        maxOverlapArea = overlapArea;
                        bestSlot = _grid[i,j];
                    }
                }
            }
        }

        if(bestSlot != null)
        {
            bestSlot.IsEmpty = false;
            return bestSlot;
        }

        return null;
    }

    //public Slot FindBestOverlappingCell(Bounds bounds)
    //{
    //    Slot bestCell = null;
    //    float maxOverlapArea = 0f;

    //    // Получаем все клетки, которые могут пересекаться
    //    List<Slot> nearbySlots = GetNearbySlots(bounds.center);

    //    foreach (Slot slot in nearbySlots)
    //    {
    //        // Получаем границы клетки
    //        Bounds cellBounds = GetCellBounds(cell.gridPosition);

    //        // Проверяем, есть ли пересечение
    //        if (!pieceBounds.Intersects(cellBounds))
    //            continue;

    //        // Вычисляем площадь пересечения
    //        float overlapArea = GetIntersectionArea(pieceBounds, cellBounds);

    //        // Запоминаем клетку с наибольшей площадью
    //        if (overlapArea > maxOverlapArea)
    //        {
    //            maxOverlapArea = overlapArea;
    //            bestCell = cell;
    //        }
    //    }

    //    return bestCell;
    //}

    //private List<Slot> GetNearbySlots(Vector2 puzzlePos)
    //{
    //    List<Slot> slots = new List<Slot>();

    //    // Оптимизация: проверяем только клетки вокруг пазла
    //    Vector2Int centerCell = WorldToGrid(puzzlePos);
    //    int radius = 2; // Радиус поиска

    //    for (int x = -radius; x <= radius; x++)
    //    {
    //        for (int y = -radius; y <= radius; y++)
    //        {
    //            Vector2Int checkPos = centerCell + new Vector2Int(x, y);
    //            if (grid.IsInsideGrid(checkPos))
    //            {
    //                slots.Add(grid.GetCell(checkPos));
    //            }
    //        }
    //    }

    //    return slots;
    //}

    public float GetIntersectionArea(Bounds a, Bounds b)
    {
        // Вычисляем ширину пересечения по X
        float overlapX = Mathf.Max(0,
            Mathf.Min(a.max.x, b.max.x) - Mathf.Max(a.min.x, b.min.x)
        );

        // Вычисляем высоту пересечения по Y
        float overlapY = Mathf.Max(0,
            Mathf.Min(a.max.y, b.max.y) - Mathf.Max(a.min.y, b.min.y)
        );

        // Площадь пересечения (для 2D)
        return overlapX * overlapY;
    }

    private void OnDrawGizmos()
    {
        // Позволяет сетке двигаться и вращаться вместе с объектом
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.red;

        // Рисуем горизонтальные линии (вдоль оси X)
        for (int y = 0; y <= _height; y++)
        {
            Gizmos.DrawLine(new Vector3(0, y * _cellSize, 0), new Vector3(_width * _cellSize, y * _cellSize, 0));
        }

        // Рисуем вертикальные линии (вдоль оси Y)
        for (int x = 0; x <= _width; x++)
        {
            Gizmos.DrawLine(new Vector3(x * _cellSize, 0, 0), new Vector3(x * _cellSize, _height * _cellSize, 0));
            
        }

        if(_grid != null)
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    Handles.Label(_grid[x, y].WorldPos, _grid[x, y].CorrectID.ToString(), new GUIStyle());
                }
            }
        }
        
    }
}
