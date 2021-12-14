using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic
{
    private IEnemySpawner _enemySpawner;
    private IResourceManager _resourceManager;
    private PointHolder _pointHolder;

    private float _delay = 3f;


    public GameLogic(PointHolder holder, IEnemySpawner spawner, IResourceManager manager)
    {
        _pointHolder = holder;
        _pointHolder.GetGameLogicTrigger().PlayerEntered += StartConstantSpawn;

        _enemySpawner = spawner;
        _resourceManager = manager;
    }

    public void Initialize()
    {
        InitializeEnemies(EEnemyType.Walker);

    }

    private void InitializeEnemies(EEnemyType enemyType)
    {
        var points = _pointHolder.GetEnemySpawnPoints();

        foreach (var point in points)
        {

            _enemySpawner.CreateEnemy(enemyType, point, GetRandomAmount());
        }
    }

    private int GetRandomAmount()
    {
        var number = Random.Range(5, 7);
        return number;
    }

    private void StartConstantSpawn()
    {
        var position = _pointHolder.GetEndlessEnemySpawnPoint();

       _enemySpawner.StartConstantSpawning(EEnemyType.Exploder, position,_delay);
    }

    public void StopConstantSpawn(bool value)
    {
        _enemySpawner.StopConstantSpawning();
        Debug.Log("game logic get called");
    }

    public void SpawnBoss()
    { 
        var bossPosition = _pointHolder.GetBossSpawnPoint();
        _enemySpawner.CreateEnemy(EEnemyType.Destructor, bossPosition, 1);
    }

}
