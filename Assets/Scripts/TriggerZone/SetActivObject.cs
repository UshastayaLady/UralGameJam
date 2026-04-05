using UnityEngine;

public class SetActivObject : MonoBehaviour, ITriggerZone
{
    [SerializeField] private GameObject activeObject;

    private void ActivationInteractiveZone()
    {
        activeObject.SetActive(!activeObject.activeSelf);
    }
    public void OnEnter()
    {
        InputPlayer.launchedInteractiveZone += ActivationInteractiveZone;
    }

    public void OnExit()
    {
        InputPlayer.launchedInteractiveZone -= ActivationInteractiveZone;
    }

    private void OnDisable()
    {
        InputPlayer.launchedInteractiveZone -= ActivationInteractiveZone;
    }
}
