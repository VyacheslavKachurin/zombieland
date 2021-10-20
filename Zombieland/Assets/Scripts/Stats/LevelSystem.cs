using System;
using UnityEngine;
public class LevelSystem
{
    public event Action<int> OnExperienceGained;
    public event Action<int, int, int> OnLevelUp;
    public event Action<int> OnUpgradePointsGained;
    public static LevelSystem LevelSystemInstance;

    private int _upgradePoints = 5;

    private int _level;
    private int _experience;
    private int _experienceToNextLevel;
    private int _experienceMultiplicator = 70;


    public LevelSystem()
    {
        LevelSystemInstance = this;

        _level = 1;
        _experience = 0;
        _experienceToNextLevel = 100;
    }
    public void AddExperience(int amount)
    {
        _experience += amount;
        int levelCounter = _level;
        if (_experience >= _experienceToNextLevel)
        {
            while (_experience >= _experienceToNextLevel)
            {

                _level++;

                OnUpgradePointsGained?.Invoke(_upgradePoints);

                _experienceToNextLevel = GetExperienceToNextLevel(_level);

                _experience -= _experienceToNextLevel;
                Debug.Log($"{ _experienceToNextLevel} { _level}");
            }
            levelCounter = _level - levelCounter;
            OnLevelUp(levelCounter, _experience, _experienceToNextLevel);

        }
        else
        {
            OnExperienceGained(amount);
        }

    }
    public int GetLevelNumber()
    {
        return _level;
    }
    public int GetExperienceToNextLevel(int level)
    {
        return level * _experienceMultiplicator; // turn into
    }

}

