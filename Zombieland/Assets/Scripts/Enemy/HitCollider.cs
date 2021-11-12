using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour
{
    private float _damageAmount;
    public float DamageAmount
    {
        get { return _damageAmount; }
        set { _damageAmount = value; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") // to avoid hitting itself
        {
            var idamageable = other.GetComponent<IDamageable>();
           
                idamageable.TakeDamage(_damageAmount);
            
        }
    }
}
