using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour, IShootingType
{
    [SerializeField] private float _fireRange = 0.05f;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private ParticleSystem _hitEffect;
    [SerializeField] private ParticleSystem _fleshEffect;// video says instantiating takes more performance then repositioning it, check with profiler in future?
    [SerializeField] private TrailRenderer _tracerEffect;


    [SerializeField] private Bullet _bullet;

    [SerializeField] private float _damageAmount = 15f;

    private Ray _ray;
    private RaycastHit _hitInfo;


    public void CreateShot(Vector3 target, Vector3 origin)
    {
        Vector3 direction = target - origin;
        _ray.origin = origin;
        _ray.direction = direction;



        for (int i = -2; i < 5; i++)
        {
            float randomRange = Random.Range(0, _fireRange);
            _ray.direction = new Vector3(
            _ray.direction.x + i * randomRange,
            _ray.direction.y,
            _ray.direction.z + i * randomRange
            );

            if (Physics.Raycast(_ray, out _hitInfo, Mathf.Infinity))
            {

                var tracer = Instantiate(_tracerEffect, _ray.origin, Quaternion.identity);
                tracer.AddPosition(_ray.origin);
              

                var enemy = _hitInfo.collider.GetComponent<IDamageable>();
                ParticleSystem effect = _hitEffect;
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
        }
    }
}
