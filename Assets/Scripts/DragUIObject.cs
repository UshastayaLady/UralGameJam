using UnityEngine;
using UnityEngine.EventSystems;

public class DragUIObject : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        
        canvas = GetComponentInParent<Canvas>();
        if (canvas == null) canvas = FindFirstObjectByType<Canvas>();
    }

    virtual public void OnBeginDrag(PointerEventData eventData)
    {
        // Добавить визуальный эффект
    }

    virtual public void OnDrag(PointerEventData eventData)
    {
        Debug.Log($"OnDrag: delta = {eventData.delta}");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}
