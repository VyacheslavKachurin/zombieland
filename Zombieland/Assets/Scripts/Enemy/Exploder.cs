using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : Enemy
{
    [SerializeField] private GameObject _explosion;
    [SerializeField] private AudioClip _explosionSound;

    private Vector3 _explosionOffset = new Vector3(0, 1, 0);
    private float _radius = 5f;
    private float _explosionForce = 1000f;
    private float _explosionDamage = 50f;
    protected new int _experience = 50;
    protected override bool IsTargetVisible()
    {
        return true;
    }

    protected override void SetAttackingState()
    {
        _navMeshAgent.enabled = false;
        _isAttacking = true;

        Explode();
    }

    private void Explode()
    {
        AudioSource.PlayClipAtPoint(_explosionSound, transform.position);
        Instantiate(_explosion, transform.position + _explosionOffset, Quaternion.identity);

        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);
        foreach (Collider collider in colliders)
        {
            Rigidbody rigidbody = collider.GetComponent<Rigidbody>();
            var damageable = collider.GetComponent<IDamageable>();
            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(_explosionForce, transform.position, _radius);

            }
            if (damageable != null)
            {
                damageable.TakeDamage(_explosionDamage);
            }
        }
        SwitchState(EnemyState.Dead);
    }

    protected override void SetDeadState()
    {
        Explode();
        Destroy(gameObject);
        Destroy(_enemyHealthBar);
        TriggerDeathEvent(_experience);
    }
}


