using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : MonoBehaviour,IWeapon
{
    public event Action OnWeaponReload;
    public event Action<int> OnBulletAmountChanged;

    public void Reload()
    {
        throw new System.NotImplementedException();
    }

    public void Shoot(bool isShooting)
    {
        Debug.Log("GrenadeLauncher");
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
