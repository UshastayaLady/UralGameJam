using System.Collections.Generic;
using UnityEngine;

public class CheckInventoryTrigger : SetActivTrigger
{
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
    }
}
