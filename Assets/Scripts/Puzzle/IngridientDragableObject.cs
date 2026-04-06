using UnityEngine;
using UnityEngine.EventSystems;

public class IngridientDragableObject : DragableObject, IBeginDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        _isDragging = true;
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);

        if (BoilerManager.Instance.IsCollideBoiler(_collider.bounds))
        {
            if (BoilerManager.Instance.IsCorrectIngredient(gameObject))
            {
                gameObject.SetActive(false);
                transform.position = _startPosition;
            }
            else
            {
                transform.position = _startPosition;
                BoilerManager.Instance.RestartMiniGame();
            }
        }
        else
        {
            transform.position = _startPosition;
        }
    }
}
