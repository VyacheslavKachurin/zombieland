using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public Equipment[] _currentEquipment;
    public static EquipmentManager Instance;

    void Start()
    {
        Instance = this;

        int numofSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        _currentEquipment = new Equipment[numofSlots];
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.EquipSlot;

        _currentEquipment[slotIndex] = newItem;

        Debug.Log($"{newItem} equipped");
    }
}
