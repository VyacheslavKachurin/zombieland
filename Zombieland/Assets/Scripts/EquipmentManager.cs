using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public Equipment[] _currentEquipment;
    public static EquipmentManager Instance;

    private EquipmentView _view;
    private Player _player;

    void Start()
    {
        Instance = this;

        int numofSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        _currentEquipment = new Equipment[numofSlots];
    }

    public void Equip(Item newItem)
    {
        _view.EquipItem(newItem);
        if (newItem.EquipSlot == EquipmentSlot.Weapon)
        {
            _player.EquipWeapon(newItem.ItemObject);
            return;
        }

        if (newItem.EquipSlot == EquipmentSlot.Helmet)
        {
            _player.EquipHelmet(newItem.ItemObject);
            return;
        }

        if (newItem.EquipSlot == EquipmentSlot.Vest)
        {
            _player.EquipVest(newItem.ItemObject);
            return;
        }
    }

    public void GetEquipmentView(EquipmentView view)
    {
        _view = view;
    }

    public void GetPlayer(Player player)
    {
        _player = player;
    }
}
