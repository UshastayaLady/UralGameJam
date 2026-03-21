using System;
using UnityEngine;
using UnityEngine.UI;

public class AnswerClick : MonoBehaviour
{
    private Button thisButton;
    private int idAnswer;

    public static event Action<int> ClickedAnswer;

    private void Awake()
    {
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(OnAnswerClick);
    }

    private void OnAnswerClick()
    {
        ClickedAnswer?.Invoke(idAnswer);
    }

    public void SetIdAnswer(int idNumber)
    {
        idAnswer = idNumber;
    }
}
