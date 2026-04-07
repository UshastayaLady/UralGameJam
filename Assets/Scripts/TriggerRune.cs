using UnityEngine;

public class TriggerRune : SetActivTrigger
{
    [Header("Настройки руны")]
    [SerializeField] private RuneType _runeType;
    [SerializeField] private float _secondsTimer;
    [SerializeField] private float _maxDrawLenght;
    [SerializeField] private float _successThreshold;

    [SerializeField] private Animator _animatorObject;

    protected override void ActivationInteractiveZone()
    {
        base.ActivationInteractiveZone();
        SpellDrawer.Instance.Initialize(_runeType, _secondsTimer, _maxDrawLenght, _successThreshold);
        SpellDrawer.Instance.OnCompleteRune += CompleteRune;
    }

    private void CompleteRune()
    {
        DeactiveNotify();
        _animatorObject.enabled = false;
        //base.ActivationInteractiveZone();
        InputPlayer.launchedInteractiveZone -= ActivationInteractiveZone;
        SpellDrawer.Instance.OnCompleteRune -= CompleteRune;
        gameObject.SetActive(false);
    }
}
