using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
public class Enemy : MonoBehaviour,IDamageable
{
    public event Action<float> OnEnemyGotAttacked;

    [SerializeField] private HitCollider _hitCollider;

    private ExperienceSystem _experienceSystem;

    private NavMeshAgent _navMeshAgent;
    private float _currentHealth;
    private float _attackRange = 1f;

    private CapsuleCollider _capsuleCollider; 
    private Animator _animator;
    private GameObject _enemyHealthBar;
    private EnemyStats _enemyStats;
    private Vector3 _offset = new Vector3(0f, 2.46f, 0f);

    private Vector3 _playerPosition;

    private int experience = 500; //move to stats??
    

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _experienceSystem = ExperienceSystem.ExperienceSystemInstance;

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
        _experienceSystem.AddExperience(experience);



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
        _enemyStats = GetComponent<EnemyStats>();

        _navMeshAgent.speed = _enemyStats.Speed.GetValue();
        _hitCollider.GetComponent<HitCollider>().DamageAmount = _enemyStats.Damage.GetValue();
        _currentHealth = _enemyStats.MaxHealth.GetValue();
        
    }

}
