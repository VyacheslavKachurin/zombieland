using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public void SetVelocity(Vector3 velocity)
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity = velocity;
    }
    private void Jump()
    {
        _rigidbody.velocity += Vector3.up*4;
        _rigidbody.detectCollisions = false;
        Explode();
    }
    private void Explode()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        Jump();
    }
}
