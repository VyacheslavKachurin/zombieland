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

    [SerializeField] protected HitCollider _hitCollider;
    [SerializeField] protected AudioClip[] _chasingSounds;

    protected NavMeshAgent _navMeshAgent;
    protected float _currentHealth;

    protected Rigidbody[] _ragdoll;

    protected CapsuleCollider _capsuleCollider;
    protected Animator _animator;
    protected AudioSource _audioSource;
    protected GameObject _enemyHealthBar;
    protected EnemyStats _enemyStats;
    protected Vector3 _offset = new Vector3(0f, 2.46f, 0f);

    protected Transform _targetTransform;

    protected int _experience = 20; //move to stats??
    protected float _visibleDistance = 10f;
    protected float _visibleAngle = 70f;
    protected float _attackRange = 1f;

    protected bool _isAttacking = false;
    protected enum EnemyState
    {
        Idle, Chasing, Attacking, Dead
    }

    protected EnemyState _currentState;

    protected void Start()
    {
        Initialize();
    }

    protected void Initialize()
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

    protected void Update()
    {
        UpdateHealthBarPosition();

        if (_currentState == EnemyState.Chasing)
        {
            RotateTowardsTarget();
        }
        CheckForTarget(IsTargetVisible(), IsTargetAttackable());
    }

    protected void SwitchState(EnemyState state)
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

    protected virtual bool IsTargetVisible()
    {
        Vector3 direction = _targetTransform.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);

        if (direction.magnitude < _visibleDistance && angle < _visibleAngle)
            return true;
        else
            return false;


    }

    protected bool IsTargetAttackable()
    {
        if (Vector3.Distance(transform.position, _targetTransform.position) <= _attackRange)
            return true;
        else
            return false;
    }

    protected void CheckForTarget(bool isVisible, bool isAttackable)
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

    protected void RotateTowardsTarget()
    {
        Vector3 direction = _targetTransform.position - transform.position;
        Quaternion desiredRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * 5f);
    }

    protected IEnumerator Move()
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

    protected virtual void SetDeadState()
    {
        _capsuleCollider.enabled = false;

        ActivateRagdoll();

        _navMeshAgent.speed = 0; // temporary fix TO

        Destroy(gameObject, 3f);
        Destroy(_enemyHealthBar);

        TriggerDeathEvent(_experience);
    }

    protected void TriggerDeathEvent(int xp)
    {
        EnemyDied?.Invoke(xp);
    }

    protected void AttackComplete() //animation event calls it
    {
        _isAttacking = false;
        _visibleAngle = 180;
    }

    public void GetHealthBar(GameObject enemyHealthBar)
    {
        _enemyHealthBar = enemyHealthBar;
    }

    protected void UpdateHealthBarPosition()
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

    protected void AssignStats()
    {
        _enemyStats = GetComponent<EnemyStats>();

        _navMeshAgent.speed = _enemyStats.Speed.GetValue();

        if (_hitCollider != null)
        {
            var collider = _hitCollider.GetComponent<HitCollider>();
            collider.DamageAmount = _enemyStats.Damage.GetValue();
        }
        var health = _enemyStats.MaxHealth.GetValue();

        _currentHealth = health;
        _enemyHealthBar.GetComponent<EnemyHealthBar>().SetMaxValue((int)health); 

    }

    protected void SetIdleState()
    {
        _animator.SetTrigger("isIdle");
        _navMeshAgent.enabled = false;
    }

    protected void SetChasingState()
    {
        _animator.ResetTrigger("Attack");
        _animator.SetTrigger("isChasing");

        PlayChasingSound();

        _navMeshAgent.enabled = true;

        StartCoroutine(Move());
    }

    protected virtual void SetAttackingState()
    {
        _animator.ResetTrigger("isChasing");
        _animator.SetTrigger("Attack");

        _navMeshAgent.enabled = false;
        _isAttacking = true;
    }

    protected void PlayChasingSound()
    {
        int random = Random.Range(0, _chasingSounds.Length);

        _audioSource.PlayOneShot(_chasingSounds[random]);
    }

    protected void ActivateRagdoll()
    {
        foreach (var rb in _ragdoll)
        {
            rb.isKinematic = false;
        }
        _animator.enabled = false;
    }

    protected void DeactivateRagdoll()
    {
        foreach (var rb in _ragdoll)
        {
            rb.isKinematic = true;
        }
        _animator.enabled = true;
    }
}
