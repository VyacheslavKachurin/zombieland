using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class SaveSystem 
{
    private GameData _gameData;

    public void SaveGame(ExperienceSystem experienceSystem, Player player,Scenes scene)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/GameData.bin";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData gameData = new GameData(experienceSystem,player,scene);

        formatter.Serialize(stream, gameData);
        stream.Close();
    }

    //public void LoadGame(ExperienceSystem experienceSystem, Player player)
    public Scenes LoadGame()
    {
        string path = Application.persistentDataPath + "/GameData.bin";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream= new FileStream(path, FileMode.Open);

            GameData gameData=formatter.Deserialize(stream) as GameData;
            _gameData = gameData;
            stream.Close();
            return _gameData.Level;
            //InsertValues(experienceSystem, player, gameData);
        }
        else
        {
            Debug.Log("save file doesnt exist");
            return Scenes.MainMenu;
        }
    }

    public void InsertValues(ExperienceSystem experienceSystem, Player player)
    {
        GameData gameData = _gameData;
        PlayerStats playerStats = player.ReturnPlayerStats();

        player.CurrentHealth = gameData.CurrentHealth;
        playerStats.MaxHealth.SetValue(gameData.MaxHealth);
        playerStats.Speed.SetValue(gameData.MovementSpeed);

        experienceSystem.XP = gameData.Experience;
        experienceSystem.Level = gameData.PlayerLevel;
        experienceSystem.UpgradePoints = gameData.UpgradePoints;

        player.transform.position = new Vector3(
            gameData.PlayerPosition[0],
            gameData.PlayerPosition[1],
            gameData.PlayerPosition[2]);
    }
}
