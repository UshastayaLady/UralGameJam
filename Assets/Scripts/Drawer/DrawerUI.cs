using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(FreezPlayer))]
public class DrawerUI : MonoBehaviour
{
    [SerializeField] private SpellDrawer _spellDrawer;

    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private TMP_Text _neededPercentText;
    [SerializeField] private TMP_Text _currentPercentText;

    [SerializeField] private TMP_Text _writeWinText;
    [SerializeField] private TMP_Text _currentPercentWinText;

    private void Initialize()
    {
        _writeWinText.gameObject.SetActive(false);
        _neededPercentText.gameObject.SetActive(true);
        _spellDrawer.gameObject.SetActive(true);

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
        _currentPercentText.text = $"Ваш результат: {percents:F1}%";
        _timerText.gameObject.SetActive(false);
    }
    private void ShowWin(string percents)
    {
        _timerText.gameObject.SetActive(false);
        _currentPercentWinText.text = $"Ваш результат: {percents}";
        _neededPercentText.gameObject.SetActive(false);
        _spellDrawer.gameObject.SetActive(false);
        _writeWinText.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        _spellDrawer.OnStartDrawing += StartDraw;
        _spellDrawer.OnDrawTimer += UpdateTimer;
        _spellDrawer.OnFinishDrawing += FinishDrawing;
        _spellDrawer.OnInitialized += Initialize;
        _spellDrawer.OnWon += ShowWin;
    }

    private void OnDisable()
    {
        _spellDrawer.OnStartDrawing -= StartDraw;
        _spellDrawer.OnDrawTimer -= UpdateTimer;
        _spellDrawer.OnFinishDrawing -= FinishDrawing;
        _spellDrawer.OnInitialized -= Initialize;
        _spellDrawer.OnWon -= ShowWin;
    }
}
