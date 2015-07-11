using UnityEngine;
using System.Collections;

public class TowerStats : MonoBehaviour
{
    //The name of the tower
    public string towerName = "";

    //How much the tower costs to build
    public int cost = 10;

    //Attack range
    public float range = 2f;

    //Current health
    public int currentHealth = 100;
    //The max health the tower can have
    public int maxHealth = 100;

    //The current level
    public int currentLevel = 1;
    //The max level of the turret
    public int maxLevel = 1;
}
