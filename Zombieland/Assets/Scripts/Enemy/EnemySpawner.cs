using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private GameObject _enemyHealthBar;
    private float _spawnRate = 2f;
    private float _spawnDistance = 20f;
    private float _range;
    private Vector3 _spawnPosition;
    private GameObject _plane;
    private Canvas _enemyCanvas;

    private Transform _playerTransform;

    private void Awake()
    {
        // ground is hardcoded, change it to something else 
        _plane = GameObject.Find("street");
        _range = _plane.GetComponent<MeshCollider>().bounds.size.x / 2;
        InvokeRepeating(nameof(SpawnEnemy), 0.1f, _spawnRate);
    }
    private void SpawnEnemy()
    {
        Enemy enemyInstance = Instantiate(_enemy, GetRandomPosition(), Quaternion.identity);
        GameObject enemyHealthBarInstance = Instantiate(_enemyHealthBar);
        enemyHealthBarInstance.SetActive(false);
        enemyHealthBarInstance.transform.SetParent(_enemyCanvas.transform, false);
        enemyInstance.GetHealthBar(enemyHealthBarInstance);
        enemyInstance.OnEnemyGotAttacked += enemyHealthBarInstance.GetComponent<EnemyHealthBar>().UpdateHealth;
        enemyInstance.GetPlayerPosition(_playerTransform);


    }
    private Vector3 GetRandomPosition()
    {
        _spawnPosition = new Vector3(
            Random.Range(-_range, _range),
            0,
            Random.Range(-_range, _range));
        if (Vector3.Distance(_playerTransform.position, _spawnPosition) >= _spawnDistance)
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
    public void GetPlayerPosition(Transform position)
    {
        _playerTransform = position;
        //  OnPlayerMoved?.Invoke(position);
    }

}
