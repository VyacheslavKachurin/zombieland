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
    public void SetVelocity(Vector3 velocity)
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity = velocity;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage();
        }
        Destroy(gameObject);
    }

}
