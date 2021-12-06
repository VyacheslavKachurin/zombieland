using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewFactory : IViewFactory
{
    private const string _viewPath = "UI/";
    private IResourceManager _resourceManager;

    public ViewFactory(IResourceManager manager)
    {
        _resourceManager = manager;
    }

    public T CreateView<T>(Eview eview)
    {
        var view=_resourceManager.CreateView<T>(_viewPath,eview);
        
        return view;
    }
}

public enum Eview { EquipmentView, InventoryView }
