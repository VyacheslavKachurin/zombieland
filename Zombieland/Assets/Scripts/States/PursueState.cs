using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueState : EnemyState
{
    private float _attackRange;// should be stored in enemy.cs
    public override void Enter(StateManager stateManager)
    {
        _attackRange = stateManager.AttackRange;
        stateManager.Enemy.SetChasingState();
    }

    public override void Exit(StateManager stateManager)
    {
      
    }

    public override void Update(StateManager stateManager)
    {
        stateManager.Enemy.Move();

        if (Vector3.Distance(stateManager.transform.position, stateManager.Target.position) <= _attackRange)
        {
           stateManager.SwitchState(stateManager._attackState);
        }
    }
}
