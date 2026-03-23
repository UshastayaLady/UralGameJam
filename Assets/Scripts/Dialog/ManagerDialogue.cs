using UnityEngine;


[RequireComponent(typeof(ViewDialogue))]
[RequireComponent(typeof(InstantiateDialogue))]
public class ManagerDialogue : MonoBehaviour
{
    private InstantiateDialogue iInstantiateDialogue;

    private void Awake()
    {
        iInstantiateDialogue = FindAnyObjectByType<InstantiateDialogue>();
    }

    private void OnEnable()
    {
        iInstantiateDialogue.Finished += CloseWindos;
    }
    public void OpenWindos(TextAsset textAsset)
    {
        iInstantiateDialogue.gameObject.SetActive(true);
        iInstantiateDialogue.StartDialogue(textAsset);
    }

    private void CloseWindos()
    {
        iInstantiateDialogue.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        iInstantiateDialogue.Finished -= CloseWindos;
    }
}
