using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : MonoBehaviour,IWeapon
{
    public event Action OnWeaponReload;
    public event Action<int> OnBulletsAmountChanged;

    event Action<bool> IWeapon.OnWeaponReload
    {
        add
        {
            throw new NotImplementedException();
        }

        remove
        {
            throw new NotImplementedException();
        }
    }

    public int BulletsAmount()
    {
        throw new NotImplementedException();
    }

    public void Reload()
    {
        throw new System.NotImplementedException();
    }

    public void Shoot(bool isShooting)
    {
        Debug.Log("GrenadeLauncher");
    }

    public Sprite WeaponIcon()
    {
        throw new NotImplementedException();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
