using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    private float _damage = 100;
    private int _lives = 3; // the best name i could come up with
    public void SetVelocity(Vector3 velocity)
    {
        GetComponent<Rigidbody>().velocity =velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        _lives--;
        var damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(_damage);
        }
        if (_lives == 0)
        {
            Destroy(gameObject);
        }
    }
}
