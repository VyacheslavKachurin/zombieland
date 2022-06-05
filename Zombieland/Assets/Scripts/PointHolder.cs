using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointHolder : MonoBehaviour
{
    [Header("Initial Spawning")]
    [SerializeField] private Transform[] _enemySpawnPoints;
    [SerializeField] private Transform _playerSpawnPoint;

    [Header("Runtime Spawning")]
    [SerializeField] private Transform _bossSpawnPoint;
    [SerializeField] private Transform _endlessEnemySpawnPoint;

    [SerializeField] private GameLogicTrigger _gameLogicTrigger;

    public Vector3 GetPlayerSpawnPoint()
    {
        return _playerSpawnPoint.position;
    }

    public Vector3[] GetEnemySpawnPoints()
    {
        Vector3[] points = new Vector3[_enemySpawnPoints.Length];

        for(int i = 0; i < points.Length; i++)
        {
            points[i] = _enemySpawnPoints[i].position;
        }

        return points;
    }

    public Vector3 GetBossSpawnPoint()
    {
        return _bossSpawnPoint.position;
    }

    public Vector3 GetEndlessEnemySpawnPoint()
    {
        return _endlessEnemySpawnPoint.position;
    }

    public GameLogicTrigger GetGameLogicTrigger()
    {
        return _gameLogicTrigger;
    }

   
}
