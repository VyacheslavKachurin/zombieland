using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState 
{

    public abstract void Enter(StateManager stateManager);

    public abstract void Update(StateManager stateManager);

    public abstract void Exit(StateManager stateManager);
 
}
