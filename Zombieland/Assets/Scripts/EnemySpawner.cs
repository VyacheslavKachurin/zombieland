using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public event Action<Vector3> OnPlayerMoved;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private GameObject _enemyHealthBar;
    private float _spawnRate = 2f;
    private float _spawnDistance = 20f;
    private float _range;
    private Vector3 _spawnPosition;
    private GameObject _plane;
    private Canvas _enemyCanvas;


    private Vector3 _playerPosition;

    private void Awake()
    {
        _plane = GameObject.Find("Plane");
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
        OnPlayerMoved += enemyInstance.GetPlayerPosition;
        
    }
    private Vector3 GetRandomPosition()
    {
        _spawnPosition = new Vector3(
            Random.Range(-_range, _range),
            0,
            Random.Range(-_range, _range));
        if (Vector3.Distance(_playerPosition, _spawnPosition) >= _spawnDistance)
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
    public void GetPlayerPosition(Vector3 position)
    {
        _playerPosition = position;
        OnPlayerMoved?.Invoke(position);
    }
  
}
