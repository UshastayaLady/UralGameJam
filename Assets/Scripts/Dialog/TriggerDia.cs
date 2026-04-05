using UnityEngine;

public class TriggerDia : MonoBehaviour, ITriggerZoneEnter
{    
    private ListDialogue _listDialogue;
    private void Awake()
    {
        _listDialogue = GetComponent<ListDialogue>();
    }
    public void OnEnter()
    {
        _listDialogue.SendAndStartDialogue();
    }
}
