using UnityEngine;
using System.Collections.Generic;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private InventoryModel _inventoryModel;
    [SerializeField] private Transform _itemsParent;
    [SerializeField] private InventorySlot _slot;

    private EquipmentModel _equipmentManager;
    private List<InventorySlot> _slots = new List<InventorySlot>();

    void Start()
    {
        _inventoryModel.ItemAdded += AddItemToUI;

        for (int i = 0; i < _inventoryModel.Capacity; i++)
        {
            CreateSlot();
        }

    }


    public void AddItemToUI(Item item)
    {
        foreach (var slot in _slots)
        {
            if (slot.Item == null)
            {
                slot.AddItem(item);

                return;
            }
        }
    }

    private void CreateSlot()
    {
        var slot = Instantiate(_slot, _itemsParent);
        _slots.Add(slot);
        slot.ItemRemoved += _inventoryModel.Remove;
        slot.ItemEquipped += _equipmentManager.Equip;
    }

    public void TogglePanel()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }

    public void GetEquipmentModel(EquipmentModel manager)
    {
        _equipmentManager = manager;
    }

}
