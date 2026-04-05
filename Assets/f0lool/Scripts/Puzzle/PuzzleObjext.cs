using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleObjext : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    [SerializeField] private int _puzzleID;
    [SerializeField] private int _correctRotation;

    private Vector2 _startPosition;

    private Collider2D _collider;

    private Slot _currentSlot;

    private float lastClickTime = 0f;
    private float doubleClickDelay = 0.3f;
    private bool isDragging = false;
    private bool isDoubleClick = false;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _startPosition = transform.position;
    }

    private void StartDrag()
    {
        if (!isDoubleClick)
        {
            isDragging = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging && !isDoubleClick)
        {
            var newPos = Camera.main.ScreenToWorldPoint(eventData.position);
            newPos.z = 0;
            transform.position = newPos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
        Bounds bounds = _collider.bounds;

        var slot = PuzzleManager.Instance.CheckPuzzleOnGrid(bounds);

        if (slot == null)
        {
            transform.position = _startPosition;
        }
        else
        {
            if (_currentSlot != null)
                _currentSlot.IsEmpty = true;

            _currentSlot = slot;
            _currentSlot.CurrentPuzzleID = _puzzleID;
            transform.position = slot.WorldPos;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        float timeSinceLastClick = Time.time - lastClickTime;

        if (timeSinceLastClick <= doubleClickDelay)
        {
            // Двойное нажатие - отменяем драг
            isDoubleClick = true;
            OnDoubleClick();
            lastClickTime = 0f;
            Debug.Log("DobleClick");
        }
        else
        {
            isDoubleClick = false;
            lastClickTime = Time.time;

            // Начинаем драг с небольшой задержкой
            StartDrag();
        }
    }

    private void OnDoubleClick()
    {
        transform.Rotate(0, 0, 90);
    }
}
