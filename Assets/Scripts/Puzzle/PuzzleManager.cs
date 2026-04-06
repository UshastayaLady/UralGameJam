using System;
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

    public event Action OnCompletePuzzle;

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
                _grid[x,y] = new Slot();
                _grid[x, y].GridPos = new Vector2Int(x,y);
                _grid[x, y].WorldPos = GridToWorld(_grid[x, y].GridPos);
                _grid[x, y].CorrectID = id;
                id++;
                if (x == Mathf.CeilToInt(_width / 2) && y == Mathf.CeilToInt(_height / 2))
                {
                    _grid[x, y].IsEmpty = false;
                    _grid[x, y].CorrectID = 0;
                    _grid[x, y].CurrentPuzzleID = 0;
                    _grid[x, y].CurrentRotatePuzzle = 0;
                }
            }
        }
    }

    public Vector2 GridToWorld(Vector2Int gridPos)
    {
        float leftBottomX = _gridOrigin.position.x + (gridPos.x * _cellSize);
        float leftBottomY = _gridOrigin.position.y + (gridPos.y * _cellSize);

        float centerX = leftBottomX + (_cellSize / 2f);
        float centerY = leftBottomY + (_cellSize / 2f);

        return new Vector2(centerX, centerY);
    }

    public Vector2Int WorldToGrid(Vector2 worldPos)
    {
        float offsetX = worldPos.x - _gridOrigin.position.x;
        float offsetY = worldPos.y - _gridOrigin.position.y;

        float gridX = (offsetX - (_cellSize / 2f)) / _cellSize;
        float gridY = (offsetY - (_cellSize / 2f)) / _cellSize;

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
            CheckCompletePuzzle();
            return bestSlot;
        }

        return null;
    }

    public float GetIntersectionArea(Bounds a, Bounds b)
    {
        float overlapX = Mathf.Max(0,
            Mathf.Min(a.max.x, b.max.x) - Mathf.Max(a.min.x, b.min.x)
        );

        float overlapY = Mathf.Max(0,
            Mathf.Min(a.max.y, b.max.y) - Mathf.Max(a.min.y, b.min.y)
        );

        return overlapX * overlapY;
    }

    public void CheckCompletePuzzle()
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                if(!_grid[i, j].IsCorrect)
                {
                    return;
                }
            }
        }

        OnCompletePuzzle?.Invoke();
        Debug.Log("Успех");
    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.red;

        for (int y = 0; y <= _height; y++)
        {
            Gizmos.DrawLine(new Vector3(0, y * _cellSize, 0), new Vector3(_width * _cellSize, y * _cellSize, 0));
        }

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
