using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
public class Enemy : MonoBehaviour
{
    public event Action<float> OnEnemyGotAttacked;

    private NavMeshAgent _navMeshAgent;
    private int _health = 5;
    private Player _player;
    private float _attackRange = 1f;
    private Animator _animator;
    private bool _isDead = false;
    private GameObject _enemyHealthBar;
    private Vector3 _offset=new Vector3(0f,2.46f,0f);
    private float _damageAmount=20f;
    private bool _isPaused = false;

    private void Start()
    {
       
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        Move();
        UpdateHealthBarPosition();
    }

    private void Move()
    {
        if (!_isPaused)
        {
            _player = FindObjectOfType<Player>();
            if (_navMeshAgent.enabled)
            {
                _navMeshAgent.SetDestination(_player.transform.position);
            }
            if (Vector3.Distance(transform.position, _player.transform.position) <= _attackRange)
            {
                Attack();
            }
        }
    }

    private void Attack()
    {
        _animator.SetTrigger("Attack");
        _navMeshAgent.enabled = false;

    }

    public void TakeDamage()
    {
        if (_health > 0)
        {
            _health--;
            if (_health == 0)
            {
                Die();
            }
            OnEnemyGotAttacked(_damageAmount);
         
        }
        else
        {
           
            Die();
        }
    }
    private void Die()
    {
        _animator.SetTrigger("Die");
        _navMeshAgent.enabled=false;
        Destroy(gameObject, 3f);
        Destroy(_enemyHealthBar) ;
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
    public void PauseGame(bool isPaused)
    {
        _isPaused = isPaused;
        if (_navMeshAgent != null)
        {
            _navMeshAgent.enabled = !isPaused;
            _animator.enabled = !isPaused;
        }
    }

 
 
   
}
