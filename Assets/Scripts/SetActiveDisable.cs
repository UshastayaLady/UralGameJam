using UnityEngine;

public class SetActiveDisable : MonoBehaviour
{
    [SerializeField] private GameObject [] _closeObjects;
    [SerializeField] private GameObject [] _openObjects;

    private void OnDisable()
    {
        if (_openObjects != null)
            foreach (var item in _openObjects) { item.gameObject.SetActive(true);}
        if (_closeObjects != null)
            foreach (var item in _closeObjects) { item.gameObject.SetActive(false);}        
    }
}
