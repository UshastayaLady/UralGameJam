using System;
using System.Collections.Generic;
using UnityEngine;

public class ListDialogue : MonoBehaviour
{
    [SerializeField] List<TextAsset> dealogues = new List<TextAsset>();
    private InstantiateDialogue instantiateDialogue;
    private ManagerDialogue dialogueManager;
    private int indexTA;

    private void Awake()
    {
        instantiateDialogue = FindAnyObjectByType<InstantiateDialogue>();
        dialogueManager = FindAnyObjectByType<ManagerDialogue>();
    }
    private void OnEnable()
    {
        instantiateDialogue.NextDialogue += NextIndexDialogue;
    }
    private void SendAndStartDialogue()
    {
        dialogueManager.OpenWindos(dealogues[indexTA]);
    }

    private void NextIndexDialogue(int index)
    {
        if (index == -1)
            indexTA++;
        else
        {
            try 
            {
                indexTA = index;
            }
            catch (IndexOutOfRangeException ex)
            {
                Debug.LogError("IndexOutOfRangeException в NextIndexDialogue в диалоге указан индекс диалога превышающий количество в листе диалогов: " + ex.Message);
            }            
        }
    }

    private void OnDisable()
    {
        instantiateDialogue.NextDialogue -= NextIndexDialogue;
    }

}
