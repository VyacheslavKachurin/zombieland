using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
public class Enemy : MonoBehaviour,IDamageable
{
    public event Action<float> OnEnemyGotAttacked;
    public event Action<int> EnemyDied;

    [SerializeField] private HitCollider _hitCollider;

    private NavMeshAgent _navMeshAgent;
    private float _currentHealth;
    private float _attackRange = 1f;

    private CapsuleCollider _capsuleCollider; 
    private Animator _animator;
    private GameObject _enemyHealthBar;
    private EnemyStats _enemyStats;
    private Vector3 _offset = new Vector3(0f, 2.46f, 0f);

    private Transform _targetTransform;

    private int experience = 20; //move to stats??
    

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _capsuleCollider = GetComponent<CapsuleCollider>();

        AssignStats();
    }

    private void Update()
    {
        Move();
        UpdateHealthBarPosition();
    }

    private void Move()
    {
        if (_navMeshAgent.enabled)
        {
            _navMeshAgent.SetDestination(_targetTransform.position);
        }
        if (Vector3.Distance(transform.position, _targetTransform.position) <= _attackRange)
        {
            Attack();
        }
    }

    private void Attack()
    {
        _animator.SetTrigger("Attack");
        _navMeshAgent.enabled = false;
    }

    public void TakeDamage(float damageAmount)
    {
        if (_currentHealth > 0)
        {
            _currentHealth -= damageAmount;
            if (_currentHealth <= 0)
            {
                Die();
            }
            OnEnemyGotAttacked?.Invoke(damageAmount);
        }
        else
        {
            Die();
        }
    }

    private void Die()
    {
        _capsuleCollider.enabled = false;
        _animator.SetTrigger("Die");
        _navMeshAgent.enabled = false;
        Destroy(gameObject, 3f);
        Destroy(_enemyHealthBar);

        EnemyDied?.Invoke(experience);
    }

    private void AttackComplete()
    {
        _navMeshAgent.enabled = true;
    }

    public void GetHealthBar(GameObject enemyHealthBar)
    {
        _enemyHealthBar = enemyHealthBar;
    }

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

}
