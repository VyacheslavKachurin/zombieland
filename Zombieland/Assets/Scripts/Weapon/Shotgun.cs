using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour,IWeapon
{
    public event Action OnWeaponReload;
    public event Action<int> OnBulletsAmountChanged;

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
        Debug.Log("Shotgun");
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
