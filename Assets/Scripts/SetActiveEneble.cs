using UnityEngine;

public class SetActiveEneble : MonoBehaviour
{
    [SerializeField] private GameObject [] _closeFridge;
    [SerializeField] private GameObject [] _openFridge;

    private void OnEnable()
    {
        foreach (var fridge in _closeFridge) { fridge.gameObject.SetActive(false);}
        foreach (var fridge in _openFridge) { fridge.gameObject.SetActive(true); }
    }
}
