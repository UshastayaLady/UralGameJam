using UnityEngine;

public class Zagluska2 : MonoBehaviour
{
    public int _count = 0;
    [SerializeField] private GameObject _gameObject;

    private void Update()
    {
        if (_count == 2)
        {
            _gameObject.gameObject.SetActive(true);
        }
    }

    public void Add()
    {
        _count++;
    }
}
