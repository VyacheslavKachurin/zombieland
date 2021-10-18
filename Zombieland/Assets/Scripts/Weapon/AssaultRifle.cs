using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : MonoBehaviour,IShootingType
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _bulletVelocity;
    public void CreateShot(Vector3 target,Vector3 gunPointPosition)
    {
        Vector3 direction = target - gunPointPosition;
        Bullet bulletInstance = Instantiate(_bullet, gunPointPosition, Quaternion.LookRotation(target));
        bulletInstance.SetVelocity(direction.normalized * _bulletVelocity);
    }
}
