using System.Collections;
using UnityEngine;

public class EnemySpawner : IEnemySpawner
{
    private IResourceManager _resourceManager;
    private IViewFactory _viewFactory;
    private Canvas _enemyCanvas;

    private float _spawnRate=1f;
    private float _spawnDistance = 20f;
    private float _range=2f;
    private Vector3 _spawnPosition;

    private Transform _targetTransform;
    private ExperienceSystem _experienceSystem;



    public EnemySpawner(IResourceManager manager, IViewFactory viewFactory)
    {
        _resourceManager = manager;
        _viewFactory = viewFactory;      
    }

    public void GetCanvas()
    {
        _enemyCanvas = _viewFactory.CreateView<Canvas>(Eview.EnemyCanvas);
    }

    public void CreateEnemy(EEnemyType type, Vector3 position,int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var randomPosition = GetRandomPosition(position);
            Enemy enemy = _resourceManager.SpawnEnemy(EEnemyType.Walker,randomPosition);
            var healthBar = _resourceManager.CreateHealthBar(_enemyCanvas.transform);

            enemy.GetHealthBar(healthBar.gameObject);

            enemy.OnEnemyGotAttacked += healthBar.UpdateHealth;
            enemy.SetTarget(_targetTransform);
            enemy.EnemyDied += _experienceSystem.AddExperience;
        }
    }

    private Vector3 GetRandomPosition(Vector3 position)
    {
        var randomPosition = position + Random.insideUnitSphere;
        return randomPosition;
    }

    public void StoreTarget(Transform position)
    {
        _targetTransform = position;
    }
    public void SetExperienceSystem(ExperienceSystem system)
    {
        _experienceSystem = system;
    }

    public void StartConstantSpawning(EEnemyType type, Vector3 position, float delay)
    {
        _resourceManager.StartCoroutine(ConstantSpawning(EEnemyType.Walker, position, delay));
    }

    private IEnumerator ConstantSpawning(EEnemyType type,Vector3 position,float delay)
    {
        while (true)
        {
            CreateEnemy(EEnemyType.Walker, position, 1);
            yield return new WaitForSeconds(delay);
        }
    }
}
public enum EEnemyType { Walker,Runner,Exploder,Destructor };
