using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum Scenes
{
    MainMenu,
    Level01,
    Level02,
    Level03,
}

public static class ScenesExtenstion
{
    public static string GetName(this Scenes scene)
    {
        return scene switch
        {
            Scenes.MainMenu => "MainMenu",
            Scenes.Level01 => "Level01",
            Scenes.Level02 => "Level02",
            Scenes.Level03 => "Level03",
            _ => throw new NotSupportedException(),
        };
    }
}
