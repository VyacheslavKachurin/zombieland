using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionEffect;
    private Rigidbody _rigidbody;
    private float _delay=0.3f;
    private float _jumpForce = 3;
    private float _radius = 3f;
    private float _explosionForce = 800f;
    private float _explosionDamage = 100;
    public void SetVelocity(Vector3 velocity)
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity = velocity;
    }

    private void Jump()
    {    
        Invoke(nameof(Explode),_delay);
    }

    private void Explode()
    {
        Instantiate(_explosionEffect, transform.position,Quaternion.identity);
        Collider[] colliders=Physics.OverlapSphere(transform.position, _radius);
        foreach(Collider collider in colliders)
        {
           Rigidbody rigidbody= collider.GetComponent<Rigidbody>();
            var damageable = collider.GetComponent<IDamageable>();
            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(_explosionForce,transform.position,_radius);
               
            }
            if (damageable != null)
            {
                damageable.TakeDamage(_explosionDamage);
            }
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Jump();
    }
}
