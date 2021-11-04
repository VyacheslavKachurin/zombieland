using UnityEngine;
public enum Difficulty
{
    Easy,
    Medium,
    Hard
}
public static class SettingsSystem
{
    public static void SetDifficulty(Difficulty difficulty)
    {
        PlayerPrefs.SetInt("Difficulty", (int)difficulty);
    }
    public static int GetDifficulty()
    {
        return PlayerPrefs.GetInt("Difficulty", 0);
    }
}


