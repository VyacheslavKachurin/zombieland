using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    private NavMeshAgent _navMeshAgent;
    private int _health = 5;
    private Player _player;
    private float _attackRange = 1f;
    private Animator _animator;
    private bool _isDead = false;
   
    private void Start()
    {
       
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
     
        _player = FindObjectOfType<Player>();
        if (_navMeshAgent.enabled)
        {
            _navMeshAgent.SetDestination(_player.transform.position);
        }
        if (Vector3.Distance(transform.position, _player.transform.position)<=_attackRange)
        {
            Attack();
        }
    }

    private void Attack()
    {
        _animator.SetTrigger("Attack");
        _navMeshAgent.enabled = false;

    }

    public void TakeDamage()
    {
        if (_health != 0)
        {
            _health--;
            Debug.Log("Im hurt");
        }
        else
        {
            Debug.Log("eeeew");
            Die();
        }
    }
    private void Die()
    {
        _animator.SetTrigger("Die");
        _navMeshAgent.enabled=false;
        Destroy(gameObject, 3f);
    }
    private void AttackComplete()
    {
        _navMeshAgent.enabled = true;
    }
 
 
   
}
