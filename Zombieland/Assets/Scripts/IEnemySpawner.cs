using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemySpawner 
{
    public void GetCanvas();

    public void CreateEnemy(EEnemyType type, Vector3 position,int amount);

    public void StoreTarget(Transform target);

    public void SetExperienceSystem(ExperienceSystem system);

    public void StartConstantSpawning(EEnemyType type, Vector3 position,float delay);
}
