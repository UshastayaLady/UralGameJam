using UnityEngine;
using UnityEngine.UI;

public class ActivatorRune : MonoBehaviour
{
    [Header("Настройки руны")]
    [SerializeField] private RuneType _runeType;
    [SerializeField] private float _secondsTimer;
    [SerializeField] private float _maxDrawLenght;
    [SerializeField] private float _successThreshold;

    [SerializeField] private GameObject _canvasDrawer;

    [SerializeField] private GameObject _text;

    public void ActivateRune()
    {
        _canvasDrawer.SetActive(true);
        SpellDrawer.Instance.Initialize(_runeType, _secondsTimer, _maxDrawLenght, _successThreshold);
        SpellDrawer.Instance.OnCompleteRune += CompleteRune;
    }

    private void CompleteRune()
    {
        _text.SetActive(true);
        _canvasDrawer.SetActive(false);
        GetComponent<Button>().gameObject.SetActive(false);
        SpellDrawer.Instance.OnCompleteRune -= CompleteRune;
    }
}
