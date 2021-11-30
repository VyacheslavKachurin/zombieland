using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Equipment", menuName ="Equipment")]
public class Equipment : Item
{
    public EquipmentSlot EquipSlot;
    public int ArmorModifier;

    public override void Use()
    {
        base.Use();
        EquipmentManager.Instance.Equip(this);
        
    }
}

public enum EquipmentSlot { Head,Chest,Legs,Weapon,Feet}
