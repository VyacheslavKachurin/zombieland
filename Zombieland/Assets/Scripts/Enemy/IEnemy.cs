using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{

    public void SetIdleState();
    public Transform GetTarget();

    public void Move();
    public void Attack();
    public float GetAttackRange();

    public void SetChasingState();

}
