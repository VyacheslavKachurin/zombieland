using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResourceManager
{
    public T CreatePrefabInstance<T>(Objects name);

    public void CreateEnvironment(Environment environment);

    public IUIRoot CreateUIRoot();

    public T CreateView<T>(string path,Eview eview);

}


