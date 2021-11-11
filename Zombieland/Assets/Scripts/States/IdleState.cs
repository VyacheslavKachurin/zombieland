using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : EnemyState
{
    private float visDist = 10f; //TODO : move to enemy.cs
    private float visAngle = 60f; //TODO : move to enemy.cs

    public override void Enter(StateManager stateManager)
    {
        stateManager.Enemy.SetIdleState();
  
    }

    public override void Update(StateManager stateManager)
    {
        Vector3 direction = stateManager.Target.position - stateManager.transform.position;
        float angle = Vector3.Angle(direction, stateManager.transform.forward);

        if (direction.magnitude < visDist && angle < visAngle)
        {
            stateManager.SwitchState(stateManager._pursueState);
        }
    }

    public override void Exit(StateManager stateManager)
    {
      
    }


}
