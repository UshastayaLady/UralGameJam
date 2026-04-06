using UnityEngine;


[RequireComponent(typeof(ViewDialogue))]
[RequireComponent(typeof(InstantiateDialogue))]
public class ManagerDialogue : MonoBehaviour
{
    [SerializeField] private GameObject _windowsDia;
    private InstantiateDialogue _iInstantiateDialogue;

    private void Awake()
    {
        _iInstantiateDialogue = FindAnyObjectByType<InstantiateDialogue>();
    }

    private void OnEnable()
    {
        _iInstantiateDialogue.Finished += CloseWindos;
    }
    public void OpenWindos(TextAsset textAsset)
    {
        _windowsDia.SetActive(true);
        _iInstantiateDialogue.StartDialogue(textAsset);
    }

    private void CloseWindos()
    {
        _windowsDia.SetActive(false);
    }

    private void OnDisable()
    {
        _iInstantiateDialogue.Finished -= CloseWindos;
    }
}
