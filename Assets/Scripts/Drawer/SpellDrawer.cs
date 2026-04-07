using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum RuneType
{
    Alohomora,      // (буква S)
    Flipendo,       // (молния)
    Lumos,          // (круг с точкой)
    Wingardium,     // (птица/волна)
    ExpectoPatronum, // (спираль)
    Incendio,       // (зигзаг)
    Stupefy,        // (звезда)
    Accio,
    Pheu
}

public class SpellDrawer : MonoBehaviour
{
    public static SpellDrawer Instance;

    [SerializeField] private RawImage _drawingCanvas;
    [SerializeField] private Color _drawColor = Color.white;
    [SerializeField] private Color _patternColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    private float _successThreshold = 70f;

    private float _maxDrawLength = 500f;
    private float _currentDrawLength = 0f;
    private float _drawingTimer;

    public event Action OnStartDrawing;
    public event Action<float> OnDrawTimer;
    public event Action<float> OnFinishDrawing;
    public event Action<float> OnContinuingDrawing;
    public event Action<string> OnWon;
    public event Action OnCompleteRune;
    public event Action OnInitialized;

    private RuneType _currentRune = RuneType.Alohomora;

    private Texture2D _drawTexture;
    private Texture2D _patternTexture;
    private List<Vector2> _drawnPoints = new List<Vector2>();
    private bool _isDrawing = false;
    private RectTransform _canvasRect;
    private List<Vector2> _patternPoints = new List<Vector2>();
    private List<bool> _patternPointHit = new List<bool>();


    public float SuccessThreshold => _successThreshold;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
            Instance = this;
        }
    }

    void Start()
    {
        if (_drawingCanvas == null)
        {
            Debug.LogError("drawingCanvas не назначен!");
            return;
        }

        _canvasRect = _drawingCanvas.GetComponent<RectTransform>();

        _drawTexture = new Texture2D(512, 512, TextureFormat.RGBA32, false);
        _drawTexture.filterMode = FilterMode.Point;
        ClearTexture(_drawTexture, Color.clear);

        _patternTexture = new Texture2D(512, 512, TextureFormat.RGBA32, false);
        _patternTexture.filterMode = FilterMode.Point;
        ClearTexture(_patternTexture, Color.clear);

        _drawingCanvas.texture = _drawTexture;

        LoadRune(_currentRune);
    }

    public void Initialize(RuneType rune, float seconds, float maxDrawLength, float successThreshold)
    {
        LoadRune(rune);
        _drawingTimer = seconds;
        _maxDrawLength = maxDrawLength;
        _successThreshold = successThreshold;

        ClearDrawing();
        _drawingCanvas.color = Color.white;

        OnInitialized?.Invoke();
    }

    private void LoadRune(RuneType rune)
    {
        _currentRune = rune;
        GeneratePatternPoints();
        DrawPattern();
        Debug.Log($"Загружена руна: {rune}");
    }

    void GeneratePatternPoints()
    {
        _patternPoints.Clear();

        List<Vector2> patternSegments = GetRunePattern(_currentRune);
        if (patternSegments.Count < 2) return;

        float stepSize = 3f;

        for (int i = 0; i < patternSegments.Count - 1; i++)
        {
            Vector2 start = patternSegments[i];
            Vector2 end = patternSegments[i + 1];
            float distance = Vector2.Distance(start, end);
            int steps = Mathf.CeilToInt(distance / stepSize);

            for (int s = 0; s <= steps; s++)
            {
                float t = (float)s / steps;
                Vector2 point = Vector2.Lerp(start, end, t);
                _patternPoints.Add(point);
            }
        }

        _patternPointHit = new List<bool>(new bool[_patternPoints.Count]);
    }

    List<Vector2> GetRunePattern(RuneType rune)
    {
        switch (rune)
        {
            case RuneType.Alohomora:
                return GetAlohomoraPattern();
            case RuneType.Flipendo:
                return GetFlipendoPattern();
            case RuneType.Lumos:
                return GetLumosPattern();
            case RuneType.Wingardium:
                return GetWingardiumPattern();
            case RuneType.ExpectoPatronum:
                return GetExpectoPatronumPattern();
            case RuneType.Incendio:
                return GetIncendioPattern();
            case RuneType.Stupefy:
                return GetStupefyPattern();
            case RuneType.Accio:
                return GetAccioPattern();
            case RuneType.Pheu:
                return GetPheuPattern();
            default:
                return GetAlohomoraPattern();
        }
    }

    List<Vector2> GetAlohomoraPattern()
    {
        return new List<Vector2>() {
            new Vector2(-240, 360), new Vector2(-240, -240),
            new Vector2(0, 120), new Vector2(240, -240),
            new Vector2(240, 360)
        }; 
    }

    List<Vector2> GetFlipendoPattern()
    {
        return new List<Vector2>() {
            new Vector2(-240, 240), new Vector2(0, 360),
            new Vector2(240, 240), new Vector2(0, -360),
            new Vector2(-150, 60), new Vector2(150, 60)
        };
    }

    List<Vector2> GetLumosPattern()
    {
        return new List<Vector2>() {
            new Vector2(-300, 300), new Vector2(0, 300),
            new Vector2(300, 0), new Vector2(0, -300),
            new Vector2(-300, -300), new Vector2(0, 0)
        };
    }

    List<Vector2> GetWingardiumPattern()
    {
        return new List<Vector2>() {
            new Vector2(-240, 360), new Vector2(240, 360),
            new Vector2(0, 0), new Vector2(240, -360),
            new Vector2(-240, -360), new Vector2(0, 0),
            new Vector2(0, 360)
        };
    }

    List<Vector2> GetExpectoPatronumPattern()
    {
        List<Vector2> spiral = new List<Vector2>();
        int turns = 3;
        int points = 60;

        for (int i = 0; i <= points; i++)
        {
            float t = (float)i / points;
            float angle = t * 2 * Mathf.PI * turns;
            float radius = 20f + t * 390f;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            spiral.Add(new Vector2(x, y));
        }

        return spiral;
    }

    List<Vector2> GetIncendioPattern()
    {
        return new List<Vector2>() {
            new Vector2(-240, 360), new Vector2(240, -360),
            new Vector2(240, 360), new Vector2(-240, -360),
            new Vector2(0, 0), new Vector2(0, -360)
        };
    }

    List<Vector2> GetStupefyPattern()
    {
        return new List<Vector2>() {
            new Vector2(-300, 360), new Vector2(300, 360),
            new Vector2(-300, 0), new Vector2(300, 0),
            new Vector2(-300, -360), new Vector2(300, -360),
            new Vector2(0, 360), new Vector2(0, -360)
        };
    }

    List<Vector2> GetAccioPattern()
    {
        List<Vector2> points = new List<Vector2>();
        // Левая часть
        points.Add(new Vector2(-300, 360));
        points.Add(new Vector2(-300, -360));
        points.Add(new Vector2(-300, 0));
        points.Add(new Vector2(0, 180));
        points.Add(new Vector2(300, 0));
        // Правая часть
        points.Add(new Vector2(300, 360));
        points.Add(new Vector2(300, -360));
        points.Add(new Vector2(0, -180));
        points.Add(new Vector2(-300, 0));
        return points;
    }

    List<Vector2> GetPheuPattern()
    {
        return new List<Vector2>()
        {
            new Vector2(-300, 300),
            new Vector2(300, 300),
            new Vector2(300, 100),
            new Vector2(-300, 100),
            new Vector2(-300, -100),
            new Vector2(300, -100),
            new Vector2(300, -300),
            new Vector2(-300, -300)
        };
    }

    void DrawPattern()
    {
        if (_patternTexture == null) return;
        ClearTexture(_patternTexture, Color.clear);

        List<Vector2> patternSegments = GetRunePattern(_currentRune);
        for (int i = 0; i < patternSegments.Count - 1; i++)
        {
            DrawLineOnTexture(_patternTexture, patternSegments[i], patternSegments[i + 1], _patternColor, 32);
        }

        _patternTexture.Apply();
        ShowPatternOverlay();

        Debug.Log($"Руна {_currentRune} загружена! Нарисовано {_patternPoints.Count} точек");
    }

    void ShowPatternOverlay()
    {
        if (_drawTexture == null || _patternTexture == null) return;

        Color[] patternPixels = _patternTexture.GetPixels();
        Color[] drawPixels = _drawTexture.GetPixels();

        for (int i = 0; i < patternPixels.Length; i++)
        {
            if (patternPixels[i].a > 0.1f)
            {
                drawPixels[i] = patternPixels[i];
            }
        }

        _drawTexture.SetPixels(drawPixels);
        _drawTexture.Apply();
    }

    void DrawLineOnTexture(Texture2D tex, Vector2 from, Vector2 to, Color color, int thickness)
    {
        if (tex == null || _canvasRect == null) return;

        Vector2 fromPixels = WorldToTexture(from);
        Vector2 toPixels = WorldToTexture(to);

        int x1 = Mathf.RoundToInt(fromPixels.x);
        int y1 = Mathf.RoundToInt(fromPixels.y);
        int x2 = Mathf.RoundToInt(toPixels.x);
        int y2 = Mathf.RoundToInt(toPixels.y);

        int dx = Mathf.Abs(x2 - x1);
        int dy = Mathf.Abs(y2 - y1);
        int sx = x1 < x2 ? 1 : -1;
        int sy = y1 < y2 ? 1 : -1;
        int err = dx - dy;

        while (true)
        {
            for (int t = -thickness / 2; t <= thickness / 2; t++)
            {
                for (int u = -thickness / 2; u <= thickness / 2; u++)
                {
                    int px = x1 + t;
                    int py = y1 + u;
                    if (px >= 0 && px < tex.width && py >= 0 && py < tex.height)
                    {
                        tex.SetPixel(px, py, color);
                    }
                }
            }

            if (x1 == x2 && y1 == y2) break;
            int e2 = 2 * err;
            if (e2 > -dy) { err -= dy; x1 += sx; }
            if (e2 < dx) { err += dx; y1 += sy; }
        }
    }

    Vector2 WorldToTexture(Vector2 worldPos)
    {
        if (_canvasRect == null) return Vector2.zero;

        Rect rect = _canvasRect.rect;
        float u = (worldPos.x + rect.width / 2f) / rect.width;
        float v = (worldPos.y + rect.height / 2f) / rect.height;

        return new Vector2(u * _drawTexture.width, v * _drawTexture.height);
    }

    void Update()
    {
        if (_drawingCanvas == null || _canvasRect == null || _drawTexture == null)
        {
            return;
        }

        Vector2 localMousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _canvasRect,
            Input.mousePosition,
            null,
            out localMousePosition
        );

        Rect rect = _canvasRect.rect;
        bool isInside = localMousePosition.x >= -rect.width / 2 && localMousePosition.x <= rect.width / 2 &&
                       localMousePosition.y >= -rect.height / 2 && localMousePosition.y <= rect.height / 2;

        if (Input.GetMouseButtonDown(0) && isInside)
        {
            StartDrawing();
            ContinueDrawing(localMousePosition);
        }
        else if (Input.GetMouseButton(0) && _isDrawing)
        {
            ContinueDrawing(localMousePosition);
        }
        else if (Input.GetMouseButtonUp(0) && _isDrawing)
        {
            FinishDrawing();
        }
    }

    private void StartDrawing()
    {
        OnStartDrawing?.Invoke();

        _isDrawing = true;
        _drawnPoints.Clear();
        _currentDrawLength = 0f;

        for (int i = 0; i < _patternPointHit.Count; i++)
        {
            _patternPointHit[i] = false;
        }

        StartCoroutine(DrawingTimer());

        Debug.Log($"Начало рисования руны {_currentRune}");
    }

    private IEnumerator DrawingTimer()
    {
        WaitForSeconds waitSeconds = new WaitForSeconds(0.1f);

        for(int i = 0; i < _drawingTimer * 10;  i++)
        {
            OnDrawTimer?.Invoke(_drawingTimer - (i * 0.1f));
            yield return waitSeconds;
        }

        FinishDrawing();
    }

    void ContinueDrawing(Vector2 newPoint)
    {
        if (_drawnPoints.Count == 0 || Vector2.Distance(_drawnPoints.Last(), newPoint) > 5f)
        {
            float newSegmentLength = 0f;
            if (_drawnPoints.Count > 0)
            {
                newSegmentLength = Vector2.Distance(_drawnPoints.Last(), newPoint);
            }

            if (_currentDrawLength + newSegmentLength > _maxDrawLength)
            {
                Debug.Log($"Достигнут лимит длины линии! ({_currentDrawLength:F1}/{_maxDrawLength})");
                FinishDrawing();
                return;
            }

            _drawnPoints.Add(newPoint);
            _currentDrawLength += newSegmentLength;

            if (_drawnPoints.Count > 1)
            {
                Color currentColor = GetCurrentLineColor();
                DrawLineOnTexture(_drawTexture, _drawnPoints[_drawnPoints.Count - 2], newPoint, currentColor, 25);
                _drawTexture.Apply();
            }

            OnContinuingDrawing?.Invoke(_maxDrawLength - _currentDrawLength);

            CheckPointHit(newPoint);
        }
    }

    Color GetCurrentLineColor()
    {
        float percentLeft = 1f - (_currentDrawLength / _maxDrawLength);
        if (percentLeft < 0.3f)
        {
            return Color.Lerp(Color.red, _drawColor, percentLeft / 0.3f);
        }
        return _drawColor;
    }

    void CheckPointHit(Vector2 point)
    {
        float hitRadius = 15f;

        for (int i = 0; i < _patternPoints.Count; i++)
        {
            if (!_patternPointHit[i])
            {
                float distance = Vector2.Distance(point, _patternPoints[i]);
                if (distance < hitRadius)
                {
                    _patternPointHit[i] = true;
                }
            }
        }
    }

    void FinishDrawing()
    {
        StopAllCoroutines();

        _isDrawing = false;

        if (_drawnPoints.Count < 5)
        {
            Debug.Log($"Слишком короткая линия! ({_drawnPoints.Count} точек)");
            FailSpell();
            return;
        }

        int hitCount = _patternPointHit.Count(h => h == true);
        float accuracy = (float)hitCount / _patternPoints.Count * 100f;



        Debug.Log($"Длина: {_currentDrawLength:F1}/{_maxDrawLength}");
        Debug.Log($"Попало в {hitCount} из {_patternPoints.Count} точек ({accuracy:F1}%)");

        if (accuracy >= _successThreshold)
        {            
            CompleteRune();
            OnWon?.Invoke($"{accuracy:F1}%/{_successThreshold}%");
        }
        else
        {
            OnFinishDrawing?.Invoke(accuracy);
            FailSpell();
        }
            
    }

    void FailSpell()
    {
        Debug.Log($"Заклинание {_currentRune} не удалось!");
        StartCoroutine(FlashRed());
    }

    void CompleteRune()
    {

        Debug.Log($"Заклинание {_currentRune} успешно!");
        StartCoroutine(FlashGreen());
        OnCompleteRune?.Invoke();
    }

    IEnumerator FlashRed()
    {
        if (_drawingCanvas != null)
        {
            _drawingCanvas.color = Color.red;
            yield return new WaitForSeconds(0.3f);
            _drawingCanvas.color = Color.white;
        }
        ClearDrawing();
    }

    IEnumerator FlashGreen()
    {
        if (_drawingCanvas != null)
        {
            _drawingCanvas.color = Color.green;
            yield return new WaitForSeconds(0.3f);
            _drawingCanvas.color = Color.white;
        }
        ClearDrawing();
    }

    void ClearDrawing()
    {
        ClearTexture(_drawTexture, Color.clear);
        ShowPatternOverlay();
        _drawnPoints.Clear();
        _currentDrawLength = 0f;
    }

    void ClearTexture(Texture2D tex, Color color)
    {
        if (tex == null) return;

        Color[] colors = new Color[tex.width * tex.height];
        for (int i = 0; i < colors.Length; i++)
            colors[i] = color;
        tex.SetPixels(colors);
        tex.Apply();
    }
}