using UnityEngine;
using UnityEngine.UI;

public class UIClose: MonoBehaviour
{

    private Button closeUI;

    private void Awake()
    {
        closeUI = GetComponent<Button>();
    }


    private void OnEnable()
    {
        if (closeUI != null)
            closeUI.onClick.AddListener(Close);
    }
    private void Close()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        if (closeUI != null)
            closeUI.onClick.RemoveListener(Close);
    }

   
}
