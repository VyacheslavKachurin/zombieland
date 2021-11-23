using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryModel : MonoBehaviour
{
    public event Action<Item> ItemAdded;

    private InventoryView _inventoryView;

    private List<Item> _items = new List<Item>();
    public int Capacity = 20;

    private void Start()
    {
        _inventoryView = GetComponent<InventoryView>();
        TogglePanel();
    }

    public bool Add(Item item)
    {       
        if (_items.Count >= Capacity)
        {
            Debug.Log("not enough space");
            return false;
        }

        _items.Add(item);
        ItemAdded?.Invoke(item);

        return true;
    }

    public void Remove(Item item)
    {
        _items.Remove(item);
    }

    public void TogglePanel()
    {
        _inventoryView.TogglePanel();
    }
}
