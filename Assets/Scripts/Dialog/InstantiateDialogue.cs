using System;
using System.Collections;
using UnityEngine;


public class InstantiateDialogue : MonoBehaviour
{
    #region variables

    [SerializeField] private ReadXmlDialogue xmlDialogue;

    public event Action<string> SaidNps;
    public event Action<string, int> Answered;
    public event Action<int> NextDialogue;
    public event Action DelledAnswersButtons;
    public event Action Finished;    

    [SerializeField] private TextAsset ta;
    private int currentNode = 0;

    #endregion
    
    private void OnEnable()
    {
        AnswerClick.ClickedAnswer += OnAnswerClicked;
    }   

    public void StartDialogue(TextAsset textAsset)
    {
        if (ta!=null)
        {            
            CleanDialogue();            
        }
        ta = textAsset;
        xmlDialogue = ReadXmlDialogue.Load(ta);
        WriteText();
    }        
   
    private void WriteText()
    {        
        SaidNps?.Invoke(xmlDialogue.nodes[currentNode].npcText);
        for (int j = 0; j < xmlDialogue.nodes[currentNode].answers.Length; j++)
        {
            Answered?.Invoke(xmlDialogue.nodes[currentNode].answers[j].text, j);
            // события на добавление ответа
        }
    }

    private void OnAnswerClicked(int numberOfButton)
    {
        DelledAnswersButtons?.Invoke();
        StartCoroutine(AnswerClicked(numberOfButton));
    }

    private IEnumerator AnswerClicked(int numberOfButton)
    {
        var answer = xmlDialogue.nodes[currentNode].answers[numberOfButton];

        if (answer == null || answer.endRestart == "true")
        {
            Finished?.Invoke();
            
            if (answer.nextDialogue != null)
            {
                if (answer.nextDialogue.nextNumberDialogue == -1)
                    NextDialogue?.Invoke(-1);
                else 
                    NextDialogue?.Invoke(answer.nextDialogue.nextNumberDialogue);
            }
            CleanDialogue();
        }
        else
        {
            currentNode = answer.nextNode;
            WriteText();
        }

        yield return new WaitForSeconds(1f);
    }  

    private void CleanDialogue()
    {
        if (xmlDialogue != null)
            xmlDialogue = null;

        currentNode = 0;
    }

    private void OnDisable()
    {
        AnswerClick.ClickedAnswer -= OnAnswerClicked;
    }

}