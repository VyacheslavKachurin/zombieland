using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour, IShootingType
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _bulletVelocity;
    public void CreateShot(Vector3 aim, Vector3 gunPointPosition)
    {
        Vector3 target = aim - gunPointPosition;
        Bullet bulletInstance = Instantiate(_bullet, gunPointPosition, Quaternion.LookRotation(target));
        bulletInstance.SetVelocity(target.normalized * _bulletVelocity);
    }
}
