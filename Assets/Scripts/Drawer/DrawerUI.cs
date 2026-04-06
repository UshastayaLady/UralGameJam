using TMPro;
using UnityEngine;

public class DrawerUI : MonoBehaviour
{
    [SerializeField] private SpellDrawer _spellDrawer;

    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private TMP_Text _neededPercentText;
    [SerializeField] private TMP_Text _currentPercentText;

    private void Initialize()
    {
        _currentPercentText.gameObject.SetActive(false);
        _neededPercentText.gameObject.SetActive(true);
        _neededPercentText.text = $"Повторите рисунок с точностью {_spellDrawer.SuccessThreshold}%";
    }

    private void StartDraw()
    {
        _timerText.gameObject.SetActive(true);  
        _currentPercentText.gameObject.SetActive(false);
    }

    private void UpdateTimer(float seconds)
    {
        _timerText.text = seconds.ToString("F1");
    }

    private void FinishDrawing(float percents)
    {
        _currentPercentText.gameObject.SetActive(true);
        _currentPercentText.text = $"Ваш результат: {percents}";
        _timerText.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _spellDrawer.OnStartDrawing += StartDraw;
        _spellDrawer.OnDrawTimer += UpdateTimer;
        _spellDrawer.OnFinishDrawing += FinishDrawing;
        _spellDrawer.OnInitialized += Initialize;
    }

    private void OnDisable()
    {
        _spellDrawer.OnStartDrawing -= StartDraw;
        _spellDrawer.OnDrawTimer -= UpdateTimer;
        _spellDrawer.OnFinishDrawing -= FinishDrawing;
        _spellDrawer.OnInitialized -= Initialize;
    }
}
