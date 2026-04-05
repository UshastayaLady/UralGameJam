using UnityEngine;

public class TriggerDia : SetActivObject
{   
    private ListDialogue _listDialogue;
    private void Awake()
    {
        _listDialogue = GetComponent<ListDialogue>();
    }

    protected override void ActivationInteractiveZone()
    {
        _listDialogue.SendAndStartDialogue();
    }
}
