using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public Equipment[] _currentEquipment;
    public static EquipmentManager Instance;

    private EquipmentView _view;

    void Start()
    {
        Instance = this;

        int numofSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        _currentEquipment = new Equipment[numofSlots];
    }

    public void Equip(Item newItem)
    {
        _view.EquipItem(newItem);
    }

    public void GetEquipmentView(EquipmentView view)
    {
        _view = view;
    }
}
