using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemySpawner 
{
    public void GetCanvas();

    public void SpawnEnemy(EnemyType type, Transform position,int count);

    public void StoreTarget(Transform target);

    public void SetExperienceSystem(ExperienceSystem system);
}
