using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IWeapon
{
    public event Action<int> OnBulletAmountChanged;
    public event Action OnWeaponReload;
    void Shoot(bool isShooting);
    void Reload();

     
    

}
