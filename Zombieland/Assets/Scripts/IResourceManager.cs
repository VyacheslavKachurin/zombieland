using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResourceManager
{
    public T CreateGameObject<T>(ESceneObjects name);

    public PointHolder CreateEnvironment(Environment environment);

    public IUIRoot CreateUIRoot();

    public T CreateView<T>(string path, Eview eview);

    public Enemy SpawnEnemy(EEnemyType type, Vector3 position);

    public EnemyHealthBar CreateHealthBar(Transform canvas);

    public Coroutine StartCoroutine(IEnumerator coroutine);

    public void StopConstantSpawning(Coroutine coroutine);

}


