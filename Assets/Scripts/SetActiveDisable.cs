using UnityEngine;

public class SetActiveDisable : MonoBehaviour
{
    [SerializeField] private GameObject [] _closeObjects;
    [SerializeField] private GameObject [] _openObjects;

    private void OnDisable()
    {
        foreach (var item in _openObjects) { item.gameObject.SetActive(true);}
        foreach (var item in _closeObjects) { item.gameObject.SetActive(false);}        
    }
}
