using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour, IResourceManager
{
    private const string _prefabsPath = "Prefabs/";
    private const string _environmentsPath = "Environments/";
    public T CreatePrefabInstance<T>(Objects name)
    {
        var obj = Instantiate(Resources.Load<GameObject>(_prefabsPath + name.ToString()));

        return obj.GetComponent<T>();
    }

    public void CreateEnvironment(Environment environment)
    {
        Instantiate(Resources.Load<GameObject>(_environmentsPath + environment.ToString()), Vector3.zero, Quaternion.identity);
    }


}
public enum Objects { InputController, EnemySpawner,Crosshair,FollowingCamera }

public enum Environment { Environment1 }


