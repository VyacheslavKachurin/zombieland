using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private void Start()
    {      
        Destroy(gameObject, 1);
       
    }
    public void SetVelocity(float velocity)
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity =transform.forward*velocity;
    }
    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage();
        }
        Destroy(gameObject);
    }

}
