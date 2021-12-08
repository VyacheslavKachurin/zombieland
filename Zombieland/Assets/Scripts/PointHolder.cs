using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointHolder : MonoBehaviour
{
    [Header("Initial Spawning")]
    [SerializeField] private Transform[] _enemySpawnPoints;
    [SerializeField] private Transform[] _weaponSpawnPoints;
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private Transform _keyItemSpawnPoint;
    [SerializeField] private Item _keyItem;


    [Header("Runtime Spawning")]
    [SerializeField] private Transform _bossSpawnPoint;
    [SerializeField] private Transform _endlessEnemySpawnPoint;

    public Vector3 GetPlayerSpawnPoint()
    {
        return _playerSpawnPoint.position;
    }

    public (Item,Transform) GetKeyItem()
    {
        return (_keyItem, _keyItemSpawnPoint);
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

    public Vector3[] GetWeaponSpawnPoints()
    {
        Vector3[] points = new Vector3[_weaponSpawnPoints.Length];

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = _weaponSpawnPoints[i].position;
        }

        return points;
    }

    public Vector3 GetEndlessEnemySpawnPoint()
    {
        return _endlessEnemySpawnPoint.position;
    }

   
}
