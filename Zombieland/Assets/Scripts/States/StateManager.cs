using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateManager : MonoBehaviour
{
    public IEnemy Enemy;

    private EnemyState _currentState;

    public EnemyState _idleState = new IdleState();
    public EnemyState _pursueState = new PursueState();
    public EnemyState _attackState = new AttackState();
    public EnemyState _deathState = new DeathState();

    public Transform Target;
    public float AttackRange;


    private NavMeshAgent _agent;
    private Animator _animator;

    private void Start()
    {
        Enemy = GetComponent<IEnemy>();
        Target = Enemy.GetTarget();
        AttackRange = Enemy.GetAttackRange();

        _currentState = _idleState;
        _currentState.Enter(this);

    }
    private void Update()
    {
        _currentState.Update(this);
    }

    public void SwitchState(EnemyState enemyState)
    {
        _currentState = enemyState;
        enemyState.Enter(this);
    }

}
