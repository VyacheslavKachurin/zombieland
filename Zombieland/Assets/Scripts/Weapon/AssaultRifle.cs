using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : MonoBehaviour, IShootingType
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _bulletVelocity;
    [SerializeField] private ParticleSystem _hitEffect;

    private Ray _ray;
    private RaycastHit _hitInfo;
    public void CreateShot(Vector3 aim, Vector3 gunPointPosition)
    {
        Vector3 target = aim - gunPointPosition;
        _ray.origin = gunPointPosition;
        _ray.direction = target;
        

        if(Physics.Raycast(_ray,out _hitInfo))
        {

            _hitEffect.transform.position = _hitInfo.point;
            _hitEffect.transform.forward = _hitInfo.normal;
            _hitEffect.Emit(1);
        }

       /*
        Bullet bulletInstance = Instantiate(_bullet, gunPointPosition, Quaternion.LookRotation(target));
        bulletInstance.SetVelocity(target.normalized * _bulletVelocity);
       */
      
    }
}
