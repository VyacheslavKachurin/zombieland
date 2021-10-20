using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerStats : CharacterStats
{
    public event Action<int> OnExperienceGained;
    public event Action<int> OnLevelUp;

    public int Experience;
    public int Level;

}
