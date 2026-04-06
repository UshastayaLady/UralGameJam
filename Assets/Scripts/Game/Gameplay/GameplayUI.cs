using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private MenuESC _menu;

    public MenuESC Menu => _menu;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            _menu.gameObject.SetActive(true);
        }
    }
}
