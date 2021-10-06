using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _enemyHealthBar;
    private float _spawnRate=2f;
    private float _spawnDistance = 20f;
    private float _range;
    private Vector3 _spawnPosition;
    private GameObject _plane;
    private Transform _player;
    private bool _isPaused=false;
    private Canvas _enemyCanvas;


    // Start is called before the first frame update
    private void Awake()
    {
        _plane = GameObject.Find("Plane");
        _range = _plane.GetComponent<MeshCollider>().bounds.size.x/2;
        InvokeRepeating(nameof(SpawnEnemy),0.1f,_spawnRate);
    }

    // Update is called once per frame
    private void Update()
    {
      
    }
    private void SpawnEnemy()
    {
        if (!_isPaused)
        {
           GameObject enemyInstance= Instantiate(_enemy, GetRandomPosition(), Quaternion.identity);
            GameObject enemyHealthBarInstance=Instantiate(_enemyHealthBar);
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
        _isPaused = isPaused;
    }
    public void SetCanvas(Canvas canvas)
    {
        _enemyCanvas = canvas;
    }
}
