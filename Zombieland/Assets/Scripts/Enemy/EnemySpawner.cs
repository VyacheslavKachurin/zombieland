using UnityEngine;

public class EnemySpawner : IEnemySpawner
{
    private IResourceManager _resourceManager;
    private IViewFactory _viewFactory;
    private GameObject _enemyCanvas;

    private float _spawnRate;
    private float _spawnDistance = 20f;
    private float _range;
    private Vector3 _spawnPosition;

    private Transform _targetTransform;
    private ExperienceSystem _experienceSystem;

    public EnemySpawner(IResourceManager manager, IViewFactory factory)
    {
        _resourceManager = manager;
        _viewFactory = factory;      
    }

    public void CreateCanvas()
    {
        _enemyCanvas = _viewFactory.CreateEnemyCanvas();
    }

    public void SpawnEnemy(EnemyType type, Transform transform)
    {
        Enemy enemy = _resourceManager.CreateEnemy(EnemyType.Walker, transform.position);
        var healthBar=CreateHealthBar(_enemyCanvas.transform, enemy);

        enemy.OnEnemyGotAttacked += healthBar.UpdateHealth;
        enemy.SetTarget(_targetTransform);
        enemy.EnemyDied += _experienceSystem.AddExperience;
    }

    private EnemyHealthBar CreateHealthBar(Transform EnemyCanvas,Enemy enemy)
    {
        var healthBar = _resourceManager.CreateHealthBar(EnemyCanvas);

        return healthBar;
        
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

    public void StoreTarget(Transform position)
    {
        _targetTransform = position;
    }
    private float SetDifficulty()
    {
        int difficulty = SettingsSystem.GetDifficulty();

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
    public void SetExperienceSystem(ExperienceSystem XPSystem)
    {
        _experienceSystem = XPSystem;
    }

}
public enum EnemyType { Walker,Runner,Destructor };
