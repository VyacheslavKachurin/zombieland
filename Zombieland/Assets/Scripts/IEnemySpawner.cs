using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemySpawner 
{
    public void GetCanvas();

    public void CreateEnemy(EnemyType type, Vector3 position,int count);

    public void StoreTarget(Transform target);

    public void SetExperienceSystem(ExperienceSystem system);
}
