using UnityEngine;

public class RaycastWeapon : MonoBehaviour, IShootingType
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private ParticleSystem _fleshImpact;
    [SerializeField] private TrailRenderer _tracerEffect;

    private float _damageAmount = 20;
    private Ray _ray;
    private RaycastHit _hitInfo;

    public void CreateShot(Vector3 target, Vector3 origin)
    {
        var direction = target - origin;
        _ray.origin = origin;
        _ray.direction = direction;

        var tracer = Instantiate(_tracerEffect, _ray.origin, Quaternion.identity);
        tracer.AddPosition(_ray.origin);

        if (Physics.Raycast(_ray, out _hitInfo, Mathf.Infinity))
        {
            Debug.Log(_hitInfo.point);
            var enemy = _hitInfo.collider.GetComponent<IDamageable>();

            if (enemy != null)
            {
                enemy.TakeDamage(_damageAmount);
                _fleshImpact.transform.position = _hitInfo.point;
                _fleshImpact.transform.forward = _hitInfo.normal;
                _fleshImpact.Emit(1);
            }
            tracer.transform.position = _hitInfo.point;
        }
    }
}
