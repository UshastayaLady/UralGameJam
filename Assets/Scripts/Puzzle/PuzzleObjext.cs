using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleObjext : DragableObject, IPointerDownHandler
{
    [SerializeField] private int _puzzleID;
    [SerializeField] private int _correctRotation;

    private bool _isDoubleClick = false;

    private Slot _currentSlot;

    private float _lastClickTime = 0f;
    private float _doubleClickDelay = 0.3f;

    private void StartDrag()
    {
        if (!_isDoubleClick)
        {
            _isDragging = true;
        }
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if(_isDoubleClick == false)
        {
            base.OnDrag(eventData);
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        Bounds bounds = _collider.bounds;

        var slot = PuzzleManager.Instance.CheckPuzzleOnGrid(bounds);

        if (_currentSlot != null)
        {
            _currentSlot.IsEmpty = true;
            _currentSlot.CurrentPuzzleID = 1000;
            _currentSlot.CurrentRotatePuzzle = 1000;
            _currentSlot = null;
        }

        if (slot == null)
        {
            transform.position = _startPosition;
        }
        else
        {
            _currentSlot = slot;
            _currentSlot.CurrentPuzzleID = _puzzleID;
            transform.position = slot.WorldPos;
            _currentSlot.CurrentRotatePuzzle = Mathf.CeilToInt(transform.rotation.eulerAngles.z);
            PuzzleManager.Instance.CheckCompletePuzzle();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        float timeSinceLastClick = Time.time - _lastClickTime;

        if (timeSinceLastClick <= _doubleClickDelay)
        {
            // Двойное нажатие - отменяем драг
            _isDoubleClick = true;
            OnDoubleClick();
            _lastClickTime = 0f;
        }
        else
        {
            _isDoubleClick = false;
            _lastClickTime = Time.time;

            // Начинаем драг с небольшой задержкой
            StartDrag();
        }
    }

    private void OnDoubleClick()
    {
        transform.Rotate(0, 0, 90);

        if (_currentSlot != null)
        {     
            _currentSlot.CurrentRotatePuzzle = Mathf.CeilToInt(transform.rotation.eulerAngles.z);
            Debug.Log(_currentSlot.CurrentRotatePuzzle);
            PuzzleManager.Instance.CheckCompletePuzzle();
        }
    }
}
