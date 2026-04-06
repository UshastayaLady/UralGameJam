using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class DragableObject : MonoBehaviour, IDragHandler, IEndDragHandler
{
    protected bool _isDragging = false;
    

    protected Vector2 _startPosition;
    protected Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _startPosition = transform.position;
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        if (_isDragging)
        {
            var newPos = Camera.main.ScreenToWorldPoint(eventData.position);
            newPos.z = 0;
            transform.position = newPos;
        }
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        _isDragging = false;
    }
}
