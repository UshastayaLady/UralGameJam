using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class AxisDragableObject : DragableObject, IBeginDragHandler
{
    public enum DragAxis
    {
        X,
        Y
    }

    [Header("Axis Settings")]
    [SerializeField] private DragAxis _axis = DragAxis.X;

    [Header("Physics Move")]
    [SerializeField] private float _moveSpeed = 12f;
    [SerializeField] private LayerMask _obstacleMask;
    [SerializeField] private float _skin = 0.01f;

    private Rigidbody2D _rb;
    private Vector3 _dragOffset;
    private float _fixedAxisValue;
    private Vector2 _targetPosition;

    private readonly RaycastHit2D[] _hits = new RaycastHit2D[10];

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();

        _rb.bodyType = RigidbodyType2D.Kinematic;
        _rb.gravityScale = 0f;
        _rb.freezeRotation = true;
        _rb.useFullKinematicContacts = true;
        _rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        _targetPosition = _rb.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _isDragging = true;

        Vector3 pointerWorldPos = GetPointerWorldPosition(eventData);
        _dragOffset = (Vector3)_rb.position - pointerWorldPos;

        if (_axis == DragAxis.X)
            _fixedAxisValue = _rb.position.y;
        else
            _fixedAxisValue = _rb.position.x;

        _targetPosition = _rb.position;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (!_isDragging)
            return;

        Vector3 pointerWorldPos = GetPointerWorldPosition(eventData);
        Vector3 targetWorldPos = pointerWorldPos + _dragOffset;

        if (_axis == DragAxis.X)
            _targetPosition = new Vector2(targetWorldPos.x, _fixedAxisValue);
        else
            _targetPosition = new Vector2(_fixedAxisValue, targetWorldPos.y);
    }

    private void FixedUpdate()
    {
        if (!_isDragging)
            return;

        Vector2 currentPos = _rb.position;
        Vector2 desiredStep = Vector2.MoveTowards(
            currentPos,
            _targetPosition,
            _moveSpeed * Time.fixedDeltaTime
        ) - currentPos;

        if (desiredStep.sqrMagnitude <= 0.000001f)
            return;

        Vector2 allowedStep = GetAllowedStep(desiredStep);
        _rb.MovePosition(currentPos + allowedStep);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        _isDragging = false;
        _targetPosition = _rb.position;
    }

    private Vector2 GetAllowedStep(Vector2 desiredStep)
    {
        float distance = desiredStep.magnitude;
        if (distance <= 0.0001f)
            return Vector2.zero;

        Vector2 direction = desiredStep.normalized;

        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = false;
        filter.SetLayerMask(_obstacleMask);

        int hitCount = _rb.Cast(direction, filter, _hits, distance + _skin);

        if (hitCount == 0)
            return desiredStep;

        float allowedDistance = distance;

        for (int i = 0; i < hitCount; i++)
        {
            RaycastHit2D hit = _hits[i];

            if (hit.collider == null)
                continue;

            if (hit.collider == _collider)
                continue;

            float candidateDistance = hit.distance - _skin;

            if (candidateDistance < allowedDistance)
                allowedDistance = candidateDistance;
        }

        allowedDistance = Mathf.Max(0f, allowedDistance);
        return direction * allowedDistance;
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