using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour, IResourceManager
{
    private const string _prefabsPath = "Prefabs/";
    private const string _environmentsPath = "Environments/";
    private const string _UIRootPath = "UI/UIRoot";
    private const string _enemyPath = "Enemies/";

    private UIRoot _root;
    public T CreateGameObject<T>(Objects name)
    {
        var obj = Instantiate(Resources.Load<GameObject>(_prefabsPath + name));

        return obj.GetComponent<T>();
    }

    public Enemy SpawnEnemy(EnemyType type, Vector3 position)
    {
        var place = new Vector3(position.x, 0.5f, position.y);
        var enemy = Instantiate(Resources.Load<GameObject>(_enemyPath + type), position, Quaternion.identity);
        return enemy.GetComponent<Enemy>();
    }

    public EnemyHealthBar CreateHealthBar(Transform canvas)
    {
        var healthBar = Instantiate(Resources.Load<GameObject>("UI/EnemyHealthBar"));
        healthBar.SetActive(false);
        healthBar.transform.SetParent(canvas,false);

        return healthBar.GetComponent<EnemyHealthBar>();
    }

    public PointHolder CreateEnvironment(Environment environment)
    {
        var pointHolder=Instantiate(Resources.Load<GameObject>(_environmentsPath + environment.ToString()), Vector3.zero, Quaternion.identity);
        return pointHolder.GetComponent<PointHolder>();
    }

    public IUIRoot CreateUIRoot()
    {
        _root = Instantiate(Resources.Load<UIRoot>(_UIRootPath));

        return _root;
    }

    public T CreateView<T>(string path, Eview eview)
    {
        var view = Instantiate(Resources.Load<GameObject>(path + eview));
        view.transform.SetParent(_root.transform);
        return view.GetComponent<T>();
    }
}
public enum Objects { InputController, Crosshair, FollowingCamera, UIRoot }

public enum Environment { Environment1 }


