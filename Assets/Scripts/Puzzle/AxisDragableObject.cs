using UnityEngine;
using UnityEngine.EventSystems;

public class AxisDragableObject : DragableObject, IBeginDragHandler
{
    public enum DragAxis
    {
        X,
        Y
    }

    [Header("Axis Settings")]
    [SerializeField] private DragAxis _axis = DragAxis.X;

    [Header("Limits")]
    [SerializeField] private bool _useLimits = false;
    [SerializeField] private float _minLimit = -5f;
    [SerializeField] private float _maxLimit = 5f;

    private Vector3 _dragOffset;
    private float _fixedAxisValue;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _isDragging = true;

        Vector3 pointerWorldPos = GetPointerWorldPosition(eventData);
        _dragOffset = transform.position - pointerWorldPos;

        if (_axis == DragAxis.X)
            _fixedAxisValue = transform.position.y;
        else
            _fixedAxisValue = transform.position.x;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (!_isDragging)
            return;

        Vector3 pointerWorldPos = GetPointerWorldPosition(eventData);
        Vector3 targetPos = pointerWorldPos + _dragOffset;

        if (_axis == DragAxis.X)
        {
            float x = targetPos.x;

            if (_useLimits)
                x = Mathf.Clamp(x, _minLimit, _maxLimit);

            transform.position = new Vector3(x, _fixedAxisValue, transform.position.z);
        }
        else
        {
            float y = targetPos.y;

            if (_useLimits)
                y = Mathf.Clamp(y, _minLimit, _maxLimit);

            transform.position = new Vector3(_fixedAxisValue, y, transform.position.z);
        }
    }


    private Vector3 GetPointerWorldPosition(PointerEventData eventData)
    {
        Camera cam = Camera.main;

        Vector3 screenPoint = eventData.position;
        screenPoint.z = Mathf.Abs(cam.transform.position.z);

        Vector3 worldPoint = cam.ScreenToWorldPoint(screenPoint);
        worldPoint.z = 0f;

        return worldPoint;
    }
}
