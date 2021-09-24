using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    private float _spawnRate=2f;
    private float _spawnDistance = 20f;
    private float _range;
    private Vector3 _spawnPosition;
    public GameObject Plane;
    private Transform _player;

    // Start is called before the first frame update
    void Awake()
    {
        _range = Plane.GetComponent<MeshCollider>().bounds.size.x/2;
        InvokeRepeating(nameof(SpawnEnemy),0.1f,_spawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMovement.IsDead)
        {
            Destroy(gameObject);
        }
    }
    private void SpawnEnemy()
    {

        Instantiate(Enemy,GetRandomPosition() , Quaternion.identity);
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
}
