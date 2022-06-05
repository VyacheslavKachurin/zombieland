using UnityEngine;
using System;

public interface IWeapon
{
    public event Action<int> OnBulletsAmountChanged;
    public event Action<bool> OnWeaponReload;

    void Shoot(bool isShooting);

    void Reload();

    void ReceiveAim(Vector3 aim);

    public Sprite WeaponIcon();

    public int ReturnBulletsAmount();

    public float SetOffset();

    public WeaponType WeaponType();

    public void Unequip();

}
