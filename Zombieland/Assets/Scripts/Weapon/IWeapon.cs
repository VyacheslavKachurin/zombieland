using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IWeapon
{
    public event Action<int> OnBulletsAmountChanged;
    public event Action OnWeaponReload;
    void Shoot(bool isShooting);
    void Reload();
    public Sprite WeaponIcon();
   // int BulletsAmount();

     
    

}
