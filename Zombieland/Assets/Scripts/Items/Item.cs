using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Item", menuName ="Item")]
public class Item : ScriptableObject
{
    public event Action CurrentAmountUpdated;

    public string Name = "New Item";
    public Sprite Icon = null;

    public EquipmentSlot EquipSlot;
    public GameObject ItemObject;

}

public enum EquipmentSlot { Weapon, Helmet, Vest, AidKit, Grenade }
