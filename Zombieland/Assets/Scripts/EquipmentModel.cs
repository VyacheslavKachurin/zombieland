using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentModel
{
    public Equipment[] _currentEquipment;

    private IEquipmentView _view;
    private Player _player;

    public EquipmentModel(IEquipmentView view,Player player)
    {
        _view = view;
        int numofSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        _currentEquipment = new Equipment[numofSlots];
        _player = player;
    }

    public EquipmentModel(EquipmentView view)
    {
        _view = view;
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

    public void SetPlayer(Player player)
    {
        _player = player;
    }


}
