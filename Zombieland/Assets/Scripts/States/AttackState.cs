using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyState
{
    private float _attackRange;
    public override void Enter(StateManager stateManager)
    {
        _attackRange = stateManager.AttackRange;
    }

    public override void Exit(StateManager stateManager)
    {

    }

    public override void Update(StateManager stateManager)
    {
        stateManager.Enemy.Attack();
        if (Vector3.Distance(stateManager.transform.position, stateManager.Target.position) >= _attackRange)
        {
            stateManager.SwitchState(stateManager._pursueState);
        }
    }
}
