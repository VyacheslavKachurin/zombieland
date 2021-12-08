using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemySpawner 
{
    public void CreateCanvas();

    public void SpawnEnemy(EnemyType type, Transform position);

    public void StoreTarget(Transform target);
}
