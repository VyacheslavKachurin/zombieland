using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    //public event Action<bool> OnGamePaused;

    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _enemyHealthBar;
    private float _spawnRate=2f;
    private float _spawnDistance = 20f;
    private float _range;
    private Vector3 _spawnPosition;
    private GameObject _plane;
    private Transform _player;
  //  private bool _isPaused=false;
    private Canvas _enemyCanvas;


    private void Awake()
    {
        _plane = GameObject.Find("Plane");
        _range = _plane.GetComponent<MeshCollider>().bounds.size.x/2;
        InvokeRepeating(nameof(SpawnEnemy),0.1f,_spawnRate);
    }

    private void Update()
    {
      
    }
    private void SpawnEnemy()
    {
      //  if (!_isPaused)
        {
           GameObject enemyInstance= Instantiate(_enemy, GetRandomPosition(), Quaternion.identity);
            GameObject enemyHealthBarInstance=Instantiate(_enemyHealthBar);
           // OnGamePaused += enemyInstance.GetComponent<Enemy>().PauseGame;
            enemyHealthBarInstance.SetActive(false);
            enemyHealthBarInstance.transform.SetParent(_enemyCanvas.transform, false);
            enemyInstance.GetComponent<Enemy>().GetHealthBar(enemyHealthBarInstance);
            enemyInstance.GetComponent<Enemy>().OnEnemyGotAttacked += enemyHealthBarInstance.GetComponent<EnemyHealthBar>().UpdateHealth;
           
        }
    }
    private Vector3 GetRandomPosition()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _spawnPosition = new Vector3(
            Random.Range(-_range,_range),
            0,
            Random.Range(-_range, _range));
        if (Vector3.Distance(_player.position,_spawnPosition)<=_spawnDistance)
        {
            return _spawnPosition;
        }
        else
        {
            return GetRandomPosition();
        }
    }
    public void StopSpawning(bool isPaused)
    {
     //   _isPaused = isPaused;
    }
    public void SetCanvas(Canvas canvas)
    {
        _enemyCanvas = canvas;
    }
    public void PauseGame(bool isPaused)
    {
      //  _isPaused = isPaused;
      //  OnGamePaused(isPaused);
    }
}
