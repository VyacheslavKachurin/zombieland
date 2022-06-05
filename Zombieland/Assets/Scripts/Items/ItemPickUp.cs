using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : Interactable
{
    [SerializeField] private Item _item;
    private InventoryModel _inventoryModel;

    private void Start()
    {
        var createdItem = Instantiate(_item.ItemObject);
        createdItem.transform.parent = transform;
        createdItem.transform.localPosition = Vector3.zero;
    }

    public override void Interact()
    {
        Unsubscribe();
        PickUp();
    }

    private void PickUp()
    {
        _inventoryModel = _player.InventoryModel;

        bool wasAdded = _inventoryModel.Add(_item);

        if (wasAdded)
        {
            Destroy(gameObject);
        }
    }
}
