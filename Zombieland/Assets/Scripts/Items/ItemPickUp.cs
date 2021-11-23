using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : Interactable
{
    [SerializeField] Item item;
    private InventoryModel _inventoryModel;
    public override void Interact()
    {
        Unsubscribe();
        PickUp();
    }

    private void PickUp()
    {
        _inventoryModel = _player.InventoryModel;

        bool wasAdded = _inventoryModel.Add(item);

        if (wasAdded)
        {
            Destroy(gameObject);
        }
    }
}
