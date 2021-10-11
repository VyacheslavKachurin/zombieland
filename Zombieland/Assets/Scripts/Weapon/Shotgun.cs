using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour,IWeapon
{
    public void Reload()
    {
        throw new System.NotImplementedException();
    }

    public void Shoot(bool isShooting)
    {
        Debug.Log("Shotgun");
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
