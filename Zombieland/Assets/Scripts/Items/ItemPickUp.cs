using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : Interactable
{
    [SerializeField] Item item;
    private InventoryModel _inventoryModel;


    private void Start()
    {
        var createdItem=Instantiate(item.ItemObject);
        createdItem.transform.parent = transform;      
        createdItem.transform.localPosition = Vector3.zero;
        createdItem.transform.localRotation = Quaternion.Euler(-90, 0, 0);


    }
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
