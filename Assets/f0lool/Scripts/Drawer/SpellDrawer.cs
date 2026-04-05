using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class SpellDrawer : MonoBehaviour
{
    public RawImage drawingCanvas;
    public Color drawColor = Color.white;
    public Color patternColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    public float successThreshold = 70f;

    [Header("Ограничения рисования")]
    public float maxDrawLength = 500f;
    public float currentDrawLength = 0f;

    [Header("Выбор руны")]
    public RuneType currentRune = RuneType.Alohomora;

    public enum RuneType
    {
        Alohomora,      // Открытие (буква S)
        Flipendo,       // Отбрасывание (молния)
        Lumos,          // Свет (круг с точкой)
        Wingardium,     // Левитация (птица/волна)
        ExpectoPatronum, // Патронус (спираль)
        Incendio,       // Огонь (зигзаг)
        Stupefy,        // Оглушение (звезда)
        Accio           // Призыв (крюк)
    }

    private Texture2D drawTexture;
    private Texture2D patternTexture;
    private List<Vector2> drawnPoints = new List<Vector2>();
    private bool isDrawing = false;
    private RectTransform canvasRect;
    private List<Vector2> patternPoints = new List<Vector2>();
    private List<bool> patternPointHit = new List<bool>();

    void Start()
    {
        if (drawingCanvas == null)
        {
            Debug.LogError("drawingCanvas не назначен!");
            return;
        }

        canvasRect = drawingCanvas.GetComponent<RectTransform>();

        drawTexture = new Texture2D(512, 512, TextureFormat.RGBA32, false);
        drawTexture.filterMode = FilterMode.Point;
        ClearTexture(drawTexture, Color.clear);

        patternTexture = new Texture2D(512, 512, TextureFormat.RGBA32, false);
        patternTexture.filterMode = FilterMode.Point;
        ClearTexture(patternTexture, Color.clear);

        drawingCanvas.texture = drawTexture;

        LoadRune(currentRune);
    }

    public void LoadRune(RuneType rune)
    {
        currentRune = rune;
        GeneratePatternPoints();
        DrawPattern();
        Debug.Log($"Загружена руна: {rune}");
    }

    void GeneratePatternPoints()
    {
        patternPoints.Clear();

        List<Vector2> patternSegments = GetRunePattern(currentRune);
        if (patternSegments.Count < 2) return;

        float stepSize = 5f;

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
                patternPoints.Add(point);
            }
        }

        patternPointHit = new List<bool>(new bool[patternPoints.Count]);
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
            default:
                return GetAlohomoraPattern();
        }
    }

    // Руна Alohomora (Открытие) - Буква "S"
    List<Vector2> GetAlohomoraPattern()
    {
        return new List<Vector2>()
        {
            new Vector2(-150, 150),
            new Vector2(150, 150),
            new Vector2(150, 50),
            new Vector2(-150, 50),
            new Vector2(-150, -50),
            new Vector2(150, -50),
            new Vector2(150, -150),
            new Vector2(-150, -150)
        };
    }

    // Руна Flipendo (Отбрасывание) - Молния
    List<Vector2> GetFlipendoPattern()
    {
        return new List<Vector2>()
        {
            new Vector2(-150, 150),
            new Vector2(-50, 150),
            new Vector2(50, -50),
            new Vector2(150, -50),
            new Vector2(150, -150),
            new Vector2(50, -150),
            new Vector2(-50, 50),
            new Vector2(-150, 50),
            new Vector2(-150, 150)
        };
    }

    // Руна Lumos (Свет) - Круг с точкой
    List<Vector2> GetLumosPattern()
    {
        List<Vector2> circle = new List<Vector2>();
        int segments = 32;
        float radius = 120f;
        float centerX = 0f;
        float centerY = 0f;

        for (int i = 0; i <= segments; i++)
        {
            float angle = i * 2 * Mathf.PI / segments;
            float x = centerX + Mathf.Cos(angle) * radius;
            float y = centerY + Mathf.Sin(angle) * radius;
            circle.Add(new Vector2(x, y));
        }

        // Добавляем точку в центре (маленький кружок)
        for (int i = 0; i <= 16; i++)
        {
            float angle = i * 2 * Mathf.PI / 16;
            float x = centerX + Mathf.Cos(angle) * 20f;
            float y = centerY + Mathf.Sin(angle) * 20f;
            circle.Add(new Vector2(x, y));
        }

        return circle;
    }

    // Руна Wingardium Leviosa (Левитация) - Волна/птица
    List<Vector2> GetWingardiumPattern()
    {
        return new List<Vector2>()
        {
            new Vector2(-180, -50),
            new Vector2(-120, 50),
            new Vector2(-60, -50),
            new Vector2(0, 50),
            new Vector2(60, -50),
            new Vector2(120, 50),
            new Vector2(180, -50),
            // Хвост птицы
            new Vector2(150, -100),
            new Vector2(120, -50),
            new Vector2(100, -80)
        };
    }

    // Руна Expecto Patronum (Патронус) - Спираль
    List<Vector2> GetExpectoPatronumPattern()
    {
        List<Vector2> spiral = new List<Vector2>();
        int turns = 3;
        int points = 60;

        for (int i = 0; i <= points; i++)
        {
            float t = (float)i / points;
            float angle = t * 2 * Mathf.PI * turns;
            float radius = 20f + t * 130f;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            spiral.Add(new Vector2(x, y));
        }

        return spiral;
    }

    // Руна Incendio (Огонь) - Зигзаг вверх
    List<Vector2> GetIncendioPattern()
    {
        return new List<Vector2>()
        {
            new Vector2(-100, -150),
            new Vector2(-50, -50),
            new Vector2(0, -150),
            new Vector2(50, -50),
            new Vector2(100, -150),
            new Vector2(80, -30),
            new Vector2(120, 50),
            new Vector2(60, 80),
            new Vector2(0, 120),
            new Vector2(-60, 80),
            new Vector2(-120, 50),
            new Vector2(-80, -30),
            new Vector2(-100, -150)
        };
    }

    // Руна Stupefy (Оглушение) - Звезда
    List<Vector2> GetStupefyPattern()
    {
        List<Vector2> star = new List<Vector2>();
        int points = 5;
        float outerRadius = 150f;
        float innerRadius = 70f;

        for (int i = 0; i <= points * 2; i++)
        {
            float angle = i * Mathf.PI / points;
            float radius = (i % 2 == 0) ? outerRadius : innerRadius;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            star.Add(new Vector2(x, y));
        }

        return star;
    }

    // Руна Accio (Призыв) - Крюк
    List<Vector2> GetAccioPattern()
    {
        return new List<Vector2>()
        {
            new Vector2(-300, 300),
            new Vector2(-100, 300),
            new Vector2(-100, 0),
            new Vector2(200, 0),
            new Vector2(300, 100),
            new Vector2(200, 200),
            new Vector2(100, 100),
            new Vector2(100, -100),
            new Vector2(-100, -100),
            new Vector2(-300, -300)
        };
    }

    void DrawPattern()
    {
        if (patternTexture == null) return;
        ClearTexture(patternTexture, Color.clear);

        List<Vector2> patternSegments = GetRunePattern(currentRune);
        for (int i = 0; i < patternSegments.Count - 1; i++)
        {
            DrawLineOnTexture(patternTexture, patternSegments[i], patternSegments[i + 1], patternColor, 8);
        }

        patternTexture.Apply();
        ShowPatternOverlay();

        Debug.Log($"Руна {currentRune} загружена! Нарисовано {patternPoints.Count} точек");
    }

    void ShowPatternOverlay()
    {
        if (drawTexture == null || patternTexture == null) return;

        Color[] patternPixels = patternTexture.GetPixels();
        Color[] drawPixels = drawTexture.GetPixels();

        for (int i = 0; i < patternPixels.Length; i++)
        {
            if (patternPixels[i].a > 0.1f)
            {
                drawPixels[i] = patternPixels[i];
            }
        }

        drawTexture.SetPixels(drawPixels);
        drawTexture.Apply();
    }

    void DrawLineOnTexture(Texture2D tex, Vector2 from, Vector2 to, Color color, int thickness)
    {
        if (tex == null || canvasRect == null) return;

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
        if (canvasRect == null) return Vector2.zero;

        Rect rect = canvasRect.rect;
        float u = (worldPos.x + rect.width / 2f) / rect.width;
        float v = (worldPos.y + rect.height / 2f) / rect.height;

        return new Vector2(u * drawTexture.width, v * drawTexture.height);
    }

    void Update()
    {
        if (drawingCanvas == null || canvasRect == null || drawTexture == null)
        {
            return;
        }

        Vector2 localMousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            Input.mousePosition,
            null,
            out localMousePosition
        );

        Rect rect = canvasRect.rect;
        bool isInside = localMousePosition.x >= -rect.width / 2 && localMousePosition.x <= rect.width / 2 &&
                       localMousePosition.y >= -rect.height / 2 && localMousePosition.y <= rect.height / 2;

        if (Input.GetMouseButtonDown(0) && isInside)
        {
            StartDrawing();
            ContinueDrawing(localMousePosition);
        }
        else if (Input.GetMouseButton(0) && isDrawing)
        {
            ContinueDrawing(localMousePosition);
        }
        else if (Input.GetMouseButtonUp(0) && isDrawing)
        {
            FinishDrawing();
        }
    }

    void StartDrawing()
    {
        isDrawing = true;
        drawnPoints.Clear();
        currentDrawLength = 0f;

        for (int i = 0; i < patternPointHit.Count; i++)
        {
            patternPointHit[i] = false;
        }

        Debug.Log($"Начало рисования руны {currentRune}");
    }

    void ContinueDrawing(Vector2 newPoint)
    {
        if (drawnPoints.Count == 0 || Vector2.Distance(drawnPoints.Last(), newPoint) > 5f)
        {
            float newSegmentLength = 0f;
            if (drawnPoints.Count > 0)
            {
                newSegmentLength = Vector2.Distance(drawnPoints.Last(), newPoint);
            }

            if (currentDrawLength + newSegmentLength > maxDrawLength)
            {
                Debug.Log($"Достигнут лимит длины линии! ({currentDrawLength:F1}/{maxDrawLength})");
                FinishDrawing();
                return;
            }

            drawnPoints.Add(newPoint);
            currentDrawLength += newSegmentLength;

            if (drawnPoints.Count > 1)
            {
                Color currentColor = GetCurrentLineColor();
                DrawLineOnTexture(drawTexture, drawnPoints[drawnPoints.Count - 2], newPoint, currentColor, 8);
                drawTexture.Apply();
            }

            CheckPointHit(newPoint);
        }
    }

    Color GetCurrentLineColor()
    {
        float percentLeft = 1f - (currentDrawLength / maxDrawLength);
        if (percentLeft < 0.3f)
        {
            return Color.Lerp(Color.red, drawColor, percentLeft / 0.3f);
        }
        return drawColor;
    }

    void CheckPointHit(Vector2 point)
    {
        float hitRadius = 15f;

        for (int i = 0; i < patternPoints.Count; i++)
        {
            if (!patternPointHit[i])
            {
                float distance = Vector2.Distance(point, patternPoints[i]);
                if (distance < hitRadius)
                {
                    patternPointHit[i] = true;
                }
            }
        }
    }

    void FinishDrawing()
    {
        isDrawing = false;

        if (drawnPoints.Count < 5)
        {
            Debug.Log($"Слишком короткая линия! ({drawnPoints.Count} точек)");
            FailSpell();
            return;
        }

        int hitCount = patternPointHit.Count(h => h == true);
        float accuracy = (float)hitCount / patternPoints.Count * 100f;

        Debug.Log($"Длина: {currentDrawLength:F1}/{maxDrawLength}");
        Debug.Log($"Попало в {hitCount} из {patternPoints.Count} точек ({accuracy:F1}%)");

        if (accuracy >= successThreshold)
            CastSpell();
        else
            FailSpell();
    }

    void FailSpell()
    {
        Debug.Log($"Заклинание {currentRune} не удалось!");
        StartCoroutine(FlashRed());
    }

    void CastSpell()
    {
        Debug.Log($"Заклинание {currentRune} успешно!");
        StartCoroutine(FlashGreen());
    }

    IEnumerator FlashRed()
    {
        if (drawingCanvas != null)
        {
            drawingCanvas.color = Color.red;
            yield return new WaitForSeconds(0.3f);
            drawingCanvas.color = Color.white;
        }
        ClearDrawing();
    }

    IEnumerator FlashGreen()
    {
        if (drawingCanvas != null)
        {
            drawingCanvas.color = Color.green;
            yield return new WaitForSeconds(0.3f);
            drawingCanvas.color = Color.white;
        }
        ClearDrawing();
    }

    void ClearDrawing()
    {
        ClearTexture(drawTexture, Color.clear);
        ShowPatternOverlay();
        drawnPoints.Clear();
        currentDrawLength = 0f;
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