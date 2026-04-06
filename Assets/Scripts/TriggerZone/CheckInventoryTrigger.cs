using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckInventoryTrigger : SetActivTrigger
{
    [SerializeField] private Image _dontFinnd;
    [SerializeField] private List<Item> _findItems;
    private static Inventory _inventory;

    private void Awake()
    {
        _inventory = FindAnyObjectByType<Inventory>();
    }

    protected override void ActivationInteractiveZone()
    {
        if (_inventory.FindItems(_findItems))
        {
            base.ActivationInteractiveZone();
        }
        else
        {
            _dontFinnd.gameObject.SetActive(true);
        }
    }   
}
