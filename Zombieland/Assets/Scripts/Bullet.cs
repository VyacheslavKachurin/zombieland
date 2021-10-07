using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Vector3 _storedVelocity;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Destroy(gameObject, 1);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage();
        }
        Destroy(gameObject);
    }
    public void PauseGame(bool isPaused)
    {
        if (_rigidbody != null)
        {
            if (isPaused)
            {
                _storedVelocity = _rigidbody.velocity;
            }
            else
            {
                _rigidbody.velocity = _storedVelocity;
            }
        }

    }
}
