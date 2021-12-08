using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResourceManager
{
    public T CreateGameObject<T>(Objects name);

    public void CreateEnvironment(Environment environment);

    public IUIRoot CreateUIRoot();

    public T CreateView<T>(string path,Eview eview);

    public Enemy CreateEnemy(EnemyType type, Vector3 position);

    public EnemyHealthBar CreateHealthBar(Transform canvas);
}


