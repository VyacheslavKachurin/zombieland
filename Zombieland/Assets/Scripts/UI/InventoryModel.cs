using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryModel
{
    public event Action<Item> ItemAdded;

    private IInventoryView _inventoryView;

    private List<Item> _items = new List<Item>();
    public int Capacity = 20;

    public InventoryModel(IInventoryView view)
    {
        _inventoryView = view;
        _inventoryView.GetInventoryModel(this);
        TogglePanel();
    }

    public bool Add(Item newItem)
    {
        if (_items.Count >= Capacity)
        {
            Debug.Log("not enough space");
            return false;
        }

        _items.Add(newItem);
        ItemAdded?.Invoke(newItem);

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
