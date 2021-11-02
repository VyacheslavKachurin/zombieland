using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private GameObject _enemyHealthBar;
    private float _spawnRate;
    private float _spawnDistance = 20f;
    private float _range;
    private Vector3 _spawnPosition;
    private GameObject _plane;
    private Canvas _enemyCanvas;

    private Transform _targetTransform;

    private void Awake()
    {
        // TODO 
        // ground is hardcoded, change it to something else 
        // will assign different Transform positions and store them in a list to randomly spawn

        _plane = GameObject.Find("street");
        _range = _plane.GetComponent<MeshCollider>().bounds.size.x / 2;

        InvokeRepeating(nameof(SpawnEnemy), 0.1f, SetDifficulty());
    }

    private void SpawnEnemy()
    {
        Enemy enemyInstance = Instantiate(_enemy, GetRandomPosition(), Quaternion.identity);
        GameObject enemyHealthBarInstance = Instantiate(_enemyHealthBar);
        enemyHealthBarInstance.SetActive(false);
        enemyHealthBarInstance.transform.SetParent(_enemyCanvas.transform, false);
        enemyInstance.GetHealthBar(enemyHealthBarInstance);
        enemyInstance.OnEnemyGotAttacked += enemyHealthBarInstance.GetComponent<EnemyHealthBar>().UpdateHealth;
        enemyInstance.SetTarget(_targetTransform);
    }

    private Vector3 GetRandomPosition()
    {
        _spawnPosition = new Vector3(
            Random.Range(-_range, _range),
            0,
            Random.Range(-_range, _range));
        if (Vector3.Distance(_targetTransform.position, _spawnPosition) >= _spawnDistance)
        {
            return _spawnPosition;
        }
        else
        {
            return GetRandomPosition();
        }
    }

    public void SetCanvas(Canvas canvas)
    {
        _enemyCanvas = canvas;
    }
    public void StopSpawning(bool uselessBool)
    {
        CancelInvoke(nameof(SpawnEnemy));
    }

    public void SetTarget(Transform position)
    {
        _targetTransform = position;
    }
    private float SetDifficulty()
    {
        int difficulty = PlayerPrefs.GetInt("Difficulty", 0);

        switch (difficulty)
        {
            case 0:
                _spawnRate = 2f;
                break;
            case 1:
                _spawnRate = 1.5f;
                break;
            case 2:
                _spawnRate = 1f;
                break;

        }
        return _spawnRate;

    }

}
