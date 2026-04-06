using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform _buttonRect;

    private void Awake()
    {
        _buttonRect = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _buttonRect.DOKill();

        _buttonRect.DOScale(1.1f, 0.2f)
            .SetEase(Ease.OutBack);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _buttonRect.DOKill();

        _buttonRect.DOScale(Vector3.one, 0.2f)
            .SetEase(Ease.InOutSine);
    }
}
