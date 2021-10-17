using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
public class Enemy : MonoBehaviour,IDamageable
{
    public event Action<float> OnEnemyGotAttacked;

    [SerializeField] private HitCollider _hitCollider;
    private NavMeshAgent _navMeshAgent;
    private float _currentHealth;
    private float _attackRange = 1f;

   
    private Animator _animator;
    private GameObject _enemyHealthBar;
    private Vector3 _offset = new Vector3(0f, 2.46f, 0f);

    private Vector3 _playerPosition;

    private CharacterStats _characterStats;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
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
            _navMeshAgent.SetDestination(_playerPosition);
        }
        if (Vector3.Distance(transform.position, _playerPosition) <= _attackRange)
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
            damageAmount = _characterStats.CalculateDamage(damageAmount);
            _currentHealth -= damageAmount;
            Debug.Log(_currentHealth);
            if (_currentHealth <= 0)
            {
                Die();
            }
            OnEnemyGotAttacked(damageAmount);
        }
        else
        {
            Die();
        }
    }
    private void Die()
    {
        _animator.SetTrigger("Die");
        _navMeshAgent.enabled = false;
        Destroy(gameObject, 3f);
        Destroy(_enemyHealthBar);
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
    public void GetPlayerPosition(Vector3 position)
    {
        _playerPosition = position;
    }
    private void AssignStats()
    {
        _characterStats = GetComponent<CharacterStats>();
        _currentHealth = _characterStats.MaxHealth.GetValue();
        _hitCollider.DamageAmount = _characterStats.damage.GetValue();
        _navMeshAgent.speed = _characterStats.movementSpeed.GetValue();
    }
}
