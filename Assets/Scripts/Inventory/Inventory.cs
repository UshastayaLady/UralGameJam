using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item> _itemsInInventory;

    private void Awake()
    {
        _itemsInInventory = new List<Item>();
    }

    private void OnEnable()
    {
        Item.tookItem += AddItem;
    }

    private void AddItem(Item newItem)
    {
        _itemsInInventory.Add(newItem);
    }

    public bool FindItems(List<Item> items)
    {
        int cointNeedFind = items.Count;
        int countFind = 0;
        foreach (Item item in items)
        {
            foreach (Item item2 in _itemsInInventory)
            {
                if (item == item2) countFind++;
            }
        }
        if (countFind == cointNeedFind) return true; else return false;
  
    }

    private void OnDisable()
    {
        Item.tookItem -= AddItem;
    }

}
