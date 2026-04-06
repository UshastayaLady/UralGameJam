using DG.Tweening;
using UnityEngine;

public class SetActivTrigger : MonoBehaviour, ITriggerZoneEnter, ITriggerZoneExit
{
    [SerializeField] protected GameObject [] _activeObjects;
    [SerializeField] protected GameObject _notifyObject;
     
    protected virtual void ActivationInteractiveZone()
    {
        for (int i = 0; i < _activeObjects.Length; i++) 
        {
            _activeObjects[i].SetActive(!_activeObjects[i].activeSelf);
        }        
    }
    public void OnEnter()
    {
        ActiveNotify();
        InputPlayer.launchedInteractiveZone += ActivationInteractiveZone;
    }

    public void OnExit()
    {
        DeactiveNotify();
        InputPlayer.launchedInteractiveZone -= ActivationInteractiveZone;
    }

    protected void OnDisable()
    {
        InputPlayer.launchedInteractiveZone -= ActivationInteractiveZone;
    }    

    protected void ActiveNotify()
    {
        if (_notifyObject == null) return;

        _notifyObject.SetActive(true);
        _notifyObject.transform.DOKill();
        _notifyObject.transform.localScale = Vector3.one;
        _notifyObject.transform.DOScale(1.3f, 0.2f).SetLoops(-1, LoopType.Yoyo);
    }

    protected void DeactiveNotify()
    {
        if (_notifyObject == null) return;


        _notifyObject.SetActive(false);
        _notifyObject.transform.DOKill();
    }
}
