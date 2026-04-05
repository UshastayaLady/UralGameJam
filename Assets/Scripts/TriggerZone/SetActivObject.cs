using UnityEngine;

public class SetActivObject : MonoBehaviour, ITriggerZone
{
    [SerializeField] private GameObject [] _activeObjects;

    private void ActivationInteractiveZone()
    {
        for (int i = 0; i < _activeObjects.Length; i++) 
        {
            _activeObjects[i].SetActive(!_activeObjects[i].activeSelf);
        }        
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
