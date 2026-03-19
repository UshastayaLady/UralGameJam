using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PoolObject))]
public class ViewDialogue : MonoBehaviour
{
    [SerializeField] private Text nodeText;
    [SerializeField] private RectTransform textScrollContent;
    [SerializeField] private ScrollRect textScrollRect;

    [SerializeField] private RectTransform answersScrollContent;


    private InstantiateDialogue instantiateDialogue;
    private PoolObject poolObject;

    private void Awake()
    {
        poolObject = GetComponent<PoolObject>();
        instantiateDialogue = GetComponent<InstantiateDialogue>();
    }

    private void OnEnable()
    {
        instantiateDialogue.SaidNps += WriteText;        
        instantiateDialogue.Answered += WriteAnswer;
        instantiateDialogue.DelledAnswersButtons += DelAnswer;
    }
    private void WriteText(string npsText)
    {
        nodeText.text = npsText;       

        float textHeight = nodeText.preferredHeight;
        textScrollContent.sizeDelta = new Vector2(textScrollContent.sizeDelta.x, textHeight + 20f);
        Canvas.ForceUpdateCanvases();

        // прокрутить вверх
        textScrollRect.verticalNormalizedPosition = 1f;
    }

    private void WriteAnswer(string answer, int idButton)
    {
        FindPool buttonAnswer = poolObject.GetObgectInPool();
        try
        {
            Text answerText = buttonAnswer.GetComponentInChildren<Text>();
            answerText.text = answer;
            float textHeight = answerText.preferredHeight;
            RectTransform buttonRect = buttonAnswer.GetComponent<RectTransform>();
            buttonRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, textHeight + 32f);
        }
        catch (NullReferenceException ex)
        {
            Debug.LogError("NullReferenceException в buttonAnswer отсутствует компонент Text: " + ex.Message);
        }

        try
        {
            buttonAnswer.GetComponent<AnswerClick>().SetIdAnswer(idButton);
            buttonAnswer.transform.SetParent(answersScrollContent, false);
            buttonAnswer.gameObject.SetActive(true);
        }
        catch (NullReferenceException ex)
        {
            Debug.LogError("NullReferenceException в buttonAnswer отсутствует компонент AnswerClick: " + ex.Message);
        }
    }

    private void DelAnswer()
    {
        FindPool[] buttons = answersScrollContent.GetComponentsInChildren<FindPool>();
        for (int i = 0; i < buttons.Length; i++)
        {
            try
            {
                buttons[i].GetComponentInChildren<Text>().text = "";
            }
            catch (NullReferenceException ex)
            {
                Debug.LogError("NullReferenceException в buttonAnswer отсутствует компонент Text: " + ex.Message);
            }
            poolObject.PutObgectInPool(buttons[i]);
        }
    }

    private void OnDisable()
    {
        instantiateDialogue.SaidNps -= WriteText;
        instantiateDialogue.Answered -= WriteAnswer;
        instantiateDialogue.DelledAnswersButtons -= DelAnswer;
    }
}
