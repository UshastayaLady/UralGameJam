using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Item : MonoBehaviour
{
    private Image _image;
    private Button take;
    [SerializeField] public string name;

    public static event Action<Item> tookItem;

    private void Awake()
    {
        _image = GetComponent<Image>();
        take = GetComponent<Button>();
    }
    private void OnEnable()
    {
        take.onClick.AddListener(TookItem);
    }
    private void TookItem()
    {
        tookItem?.Invoke(this);
        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        take.onClick.RemoveListener(TookItem);
    }
}
