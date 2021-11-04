using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public Scenes Level;

    public float CurrentHealth;
    public float MaxHealth;
    public float MovementSpeed;

    public int Experience;
    public int PlayerLevel;
    public int UpgradePoints;

    public float[] PlayerPosition=new float[3];

    public GameData(ExperienceSystem experienceSystem,Player player,Scenes scene)
    {
        PlayerStats playerStats = player.ReturnPlayerStats();

        CurrentHealth = player.CurrentHealth;
        MaxHealth = playerStats.MaxHealth.GetValue();
        MovementSpeed= playerStats.Speed.GetValue();

        Experience = experienceSystem.XP;
        PlayerLevel = experienceSystem.Level;
        UpgradePoints = experienceSystem.UpgradePoints;

        PlayerPosition[0] = player.transform.position.x;
        PlayerPosition[1] = player.transform.position.y;
        PlayerPosition[2] = player.transform.position.z;

        // Level = scene.GetName();

        Level = scene;
        
    }
   
}
