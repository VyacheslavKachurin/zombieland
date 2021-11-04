using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int _damageAmount = 25;
    private Rigidbody _rigidbody;
    private void Start()
    {      
        Destroy(gameObject, 1);
       
    }

    public void SetVelocity(Vector3 velocity)
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity =velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(_damageAmount);
        }
      //  Destroy(gameObject);
    }

}
