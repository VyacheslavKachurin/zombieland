using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic
{
    private IEnemySpawner _enemySpawner;
    private IResourceManager _resourceManager;
    private PointHolder _pointHolder;

    public GameLogic(PointHolder holder, IEnemySpawner spawner, IResourceManager manager)
    {
        _pointHolder = holder;
        _enemySpawner = spawner;
        _resourceManager = manager;
    }

    public void Initialize()
    {
        CreateEnemies();
    }

    private void CreateEnemies()
    {
        var points = _pointHolder.GetEnemySpawnPoints();

        foreach (var point in points)
        {
           
            _enemySpawner.CreateEnemy(EEnemyType.Walker,point, GetRandomAmount());
        }
    }

    private int GetRandomAmount()
    {
        var number = Random.Range(5, 7);
        return number;
    }


}
