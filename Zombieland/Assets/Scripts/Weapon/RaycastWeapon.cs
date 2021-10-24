using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour, IShootingType
{
    [SerializeField] private LayerMask _layer;
    [SerializeField] private ParticleSystem _hitEffect;
    [SerializeField] private ParticleSystem _fleshEffect;// video says instantiating takes more performance then repositioning it, check with profiler in future?
    [SerializeField] private TrailRenderer _tracerEffect;

    [SerializeField] private float _damageAmount = 15f;

    private Ray _ray;
    private RaycastHit _hitInfo;


    public void CreateShot(Vector3 aim, Vector3 gunPointPosition)
    {
        Vector3 target = aim - gunPointPosition;
        _ray.origin = gunPointPosition;
        _ray.direction = target;

        var tracer = Instantiate(_tracerEffect, _ray.origin, Quaternion.identity);
        tracer.AddPosition(_ray.origin);

        if (Physics.Raycast(_ray, out _hitInfo, _layer))
        {


            var enemy = _hitInfo.collider.GetComponent<IDamageable>();
            ParticleSystem effect = _hitEffect;
            Debug.DrawLine(_ray.origin, _hitInfo.point, Color.red, 2f);
            if (enemy != null)
            {
                effect = _fleshEffect;
                enemy.TakeDamage(_damageAmount);
            }
            else
            {
                effect = _hitEffect;
            }

            effect.transform.position = _hitInfo.point;
            effect.transform.forward = _hitInfo.normal;
            effect.Emit(1);


            tracer.transform.position = _hitInfo.point;
        }

        /*
         Bullet bulletInstance = Instantiate(_bullet, gunPointPosition, Quaternion.LookRotation(target));
         bulletInstance.SetVelocity(target.normalized * _bulletVelocity);
        */

    }
}
