using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentView : MonoBehaviour
{
    [SerializeField] private InventorySlot _slot;
    [SerializeField] private RectTransform _slotParent;

    private InventoryModel _inventoryModel;

    private List<InventorySlot> _slots = new List<InventorySlot>();
    void Start()
    {
        int length = System.Enum.GetNames(typeof(EquipmentSlot)).Length;

        for (int i = 0; i < length; i++)
        {
            CreateSlot();
        }
    }

    private void CreateSlot()
    {
        var slot = Instantiate(_slot, _slotParent);

        _slots.Add(slot);
    }

    public void EquipItem(Item item)
    {
        int slotIndex = (int)item.EquipSlot;

        var slot = _slots[slotIndex];
        if (slot.Item != null)
        {
            _inventoryModel.Add(slot.Item);
            slot.ClearSlot();
        }

        slot.AddItem(item);

        // slot.Disable();
    }

    public void GetInventoryModel(InventoryModel model)
    {
        _inventoryModel = model;
    }

    public void ToggleButtons()
    {
        foreach (var slot in _slots)
        {
            if (slot.Item != null)
            {
                slot.ToggleRemoveButton();
            }
        }
    }
}
