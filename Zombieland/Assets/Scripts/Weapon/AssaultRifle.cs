using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : MonoBehaviour,IWeapon
{
    private float _velocity = 20f;
    private float _firingRate = 0.2f;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _gunPoint;
    private Coroutine _shootingCoroutine;
    public void Reload()
    {
        
    }

    public void Shoot(bool isShooting)
    {
        if (isShooting)
        {
            _shootingCoroutine = StartCoroutine(Shooting());
        }
        else
        {
            StopCoroutine(_shootingCoroutine);
            Debug.Log("stopped");
        }
    }
    private IEnumerator Shooting()
    {
        while (true)
        {
            Bullet bulletInstance = Instantiate(_bullet, _gunPoint.position, _gunPoint.rotation);
            bulletInstance.SetVelocity(_velocity);
            Debug.Log("one shot");
            yield return new WaitForSeconds(_firingRate);
        }
    }
}
