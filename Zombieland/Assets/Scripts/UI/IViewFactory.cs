using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IViewFactory 
{
    public T CreateView<T>(Eview eview);
}
