using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : Enemy
{
    [SerializeField] private GameObject _explosion;

    private Vector3 _explosionOffset = new Vector3(0, 1, 0);
    protected override bool IsTargetVisible()
    {
        return true;
    }

    protected override void SetAttackingState()
    {
        _navMeshAgent.enabled = false;
        _isAttacking = true;

        Explode();
    }

    private void Explode()
    {
        Instantiate(_explosion, transform.position+_explosionOffset,Quaternion.identity);
       
    }

}
