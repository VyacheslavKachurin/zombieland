using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour, IDamageable
{
    public event Action<float> OnEnemyGotAttacked;
    public event Action<int> EnemyDied;

    [SerializeField] private HitCollider _hitCollider;
    [SerializeField] private AudioClip[] _chasingSounds;

    private NavMeshAgent _navMeshAgent;
    private float _currentHealth;

    private Rigidbody[] _ragdoll;

    private CapsuleCollider _capsuleCollider;
    private Animator _animator;
    private AudioSource _audioSource;
    private GameObject _enemyHealthBar;
    private EnemyStats _enemyStats;
    private Vector3 _offset = new Vector3(0f, 2.46f, 0f);

    private Transform _targetTransform;

    private int _experience = 20; //move to stats??
    private float _visibleDistance = 10f;
    private float _visibleAngle = 70f;
    private float _attackRange = 1f;

    private bool _isAttacking = false;
    private enum EnemyState
    {
        Idle, Chasing, Attacking, Dead
    }

    private EnemyState _currentState;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _ragdoll = GetComponentsInChildren<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();

        DeactivateRagdoll();
        AssignStats();

        SwitchState(EnemyState.Idle);
    }

    private void Update()
    {
        UpdateHealthBarPosition();

        if (_currentState == EnemyState.Chasing)
        {
            RotateTowardsTarget();
        }
        CheckForTarget(IsTargetVisible(),IsTargetAttackable());
    }

    private void SwitchState(EnemyState state)
    {
        if (_currentState != state)
        {
            _currentState = state;

            switch (state)
            {
                case EnemyState.Idle:
                    SetIdleState();
                    return;

                case EnemyState.Chasing:
                    SetChasingState();
                    return;

                case EnemyState.Attacking:
                    SetAttackingState();
                    return;
                
                case EnemyState.Dead:
                    SetDeadState();
                    return;

                default:
                    return;
            }
        }
    }

    private bool IsTargetVisible()
    {
        Vector3 direction = _targetTransform.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);

        if (direction.magnitude < _visibleDistance && angle < _visibleAngle)
            return true;
        else
            return false;


    }

    private bool IsTargetAttackable()
    {
        if (Vector3.Distance(transform.position, _targetTransform.position) <= _attackRange)
            return true;
        else
            return false;
    }

    private void CheckForTarget(bool isVisible,bool isAttackable)
    {
        if (!_isAttacking)
        {
            if (isVisible && isAttackable)
            {
                SwitchState(EnemyState.Attacking);
            }
            else if (isVisible)
            {
                SwitchState(EnemyState.Chasing);
            }
            else
            {
                SwitchState(EnemyState.Idle);
            }
        }
    }

    private void RotateTowardsTarget()
    {
        Vector3 direction = _targetTransform.position - transform.position;
        Quaternion desiredRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * 5f);
    }

    private IEnumerator Move()
    {
        while (_currentState == EnemyState.Chasing)
        {
            _navMeshAgent.SetDestination(_targetTransform.position);
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        if (_currentHealth > 0)
        {
            _currentHealth -= damageAmount;
            if (_currentHealth <= 0)
            {
                SwitchState(EnemyState.Dead);
            }
            OnEnemyGotAttacked?.Invoke(damageAmount);
        }
        else
        {
            SwitchState(EnemyState.Dead);
        }
    }

    private void SetDeadState()
    {
        _capsuleCollider.enabled = false;

        ActivateRagdoll();
        
        _navMeshAgent.speed = 0; // temporary fix TO

        Destroy(gameObject, 3f);
        Destroy(_enemyHealthBar);

        EnemyDied?.Invoke(_experience);
    }

    private void AttackComplete() //animation event calls it
    {
        _isAttacking = false;
        _visibleAngle = 180;
    }

    //public void GetHealthBar(GameObject enemyHealthBar)
    //{
    //   _enemyHealthBar = enemyHealthBar;
    //}

    private void UpdateHealthBarPosition()
    {
        if (_enemyHealthBar != null)
        {
            _enemyHealthBar.transform.position = Camera.main.WorldToScreenPoint(_offset + transform.position);

            // avoid checking every frame, do it only once
            if (!_enemyHealthBar.activeInHierarchy)
            {
                _enemyHealthBar.SetActive(true);
            }
        }

    }

    public void SetTarget(Transform position)
    {
        _targetTransform = position;
    }

    private void AssignStats()
    {
        _enemyStats = GetComponent<EnemyStats>();

        _navMeshAgent.speed = _enemyStats.Speed.GetValue();
        _hitCollider.GetComponent<HitCollider>().DamageAmount = _enemyStats.Damage.GetValue();
        _currentHealth = _enemyStats.MaxHealth.GetValue();

    }

    private void SetIdleState()
    {
        _animator.SetTrigger("isIdle");
        _navMeshAgent.enabled = false;
    }

    private void SetChasingState()
    {
        _animator.ResetTrigger("Attack");
        _animator.SetTrigger("isChasing");

        PlayChasingSound();

        _navMeshAgent.enabled = true;

        StartCoroutine(Move());
    }

    private void SetAttackingState()
    {
        _animator.ResetTrigger("isChasing");
        _animator.SetTrigger("Attack");

        _navMeshAgent.enabled = false;
        _isAttacking = true;
    }

    private void PlayChasingSound()
    {
        int random = Random.Range(0, _chasingSounds.Length);

        _audioSource.PlayOneShot(_chasingSounds[random]);
    }

    private void ActivateRagdoll()
    {
        foreach (var rb in _ragdoll)
        {
            rb.isKinematic = false;
        }
        _animator.enabled = false;
    }

    private void DeactivateRagdoll()
    {
        foreach (var rb in _ragdoll)
        {
            rb.isKinematic = true;
        }
        _animator.enabled = true;
    }
}
