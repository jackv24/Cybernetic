﻿using UnityEngine;
using System.Collections;

public class TowerStats : MonoBehaviour
{
    //The name of the tower
    public string towerName = "";

    //Current health
    public int currentHealth = 100;

    //The current level
    public int currentLevel = 0;

    public Tower[] levels;

    public void Upgrade()
    {
        if (currentLevel + 1 < levels.Length)
        {
            currentLevel++;

            // Adds the health difference between levels to the tower's current health
            currentHealth += (levels[currentLevel].maxHealth - levels[currentLevel - 1].maxHealth);
        }
    }
}

[System.Serializable]
public class Tower
{
    //How much the tower costs to build
    public int cost = 10;

    //Attack speed
    public float speed = 2.0f;
    //Attack range
    public float range = 2.0f;

    //The max health the tower can have
    public int maxHealth = 100;
}
