using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ExperienceSystem
{
    public static ExperienceSystem ExperienceSystemInstance;
    public event Action<int> OnUpgradePointsChanged;

    public event Action<int> OnXPGained;
    public event Action<int, int, int> OnLevelUp;

    public int XP
    {
        get { return _xp; }
        set { _xp = value; }
    }
    public int Level
    {
        get { return _level; }
        set { _level = value; OnLevelUp(value, _xp, _experienceToNextLevel); }
    }
    public int UpgradePoints
    {
        get { return _upgradePoints; }
        set { _upgradePoints = value; OnUpgradePointsChanged(value); }
    }

    private int _xp;
    private int _level;
    private int _experienceToNextLevel;

    private int _upgradePoints;
    private int _pointsPerLevel = 5;

    public ExperienceSystem(int level = 1, int xp = 0)
    {
        // 1. enemy.cs uses AddExperience on its death;
        // 2. UpgradeDisplay updates UpgradePoints from its event;
        // 3. Plus buttons decrease update points;
        // 4. Level controller takes it for saving;

        ExperienceSystemInstance = this;

        _level = level;
        _xp = xp;
        _experienceToNextLevel = GetExperienceToNextLevel();
    }
    public void AddExperience(int xp)
    {
        _xp += xp;

        if (_xp < _experienceToNextLevel)
        {
            OnXPGained?.Invoke(xp);
        }
        else
        {
            while (_xp >= _experienceToNextLevel)
            {
                _xp = _xp - _experienceToNextLevel;
                _level++; //can add only two levels at a time, if more, it adds too many XP;

                _upgradePoints += _pointsPerLevel;

                OnUpgradePointsChanged(_upgradePoints); // add checking for null

                _experienceToNextLevel = GetExperienceToNextLevel();
                OnLevelUp(_level, _xp, _experienceToNextLevel);
                Debug.Log("level up");
            }
        }

    }
    public int GetExperienceToNextLevel()
    {
        int level = _level + 1;
        int maxXp = (int)Mathf.Floor(100 * level * Mathf.Pow(level, 0.5f));

        return maxXp;
    }
    public void UsePoint()
    {
        _upgradePoints--;
        OnUpgradePointsChanged(_upgradePoints);
    }
}
