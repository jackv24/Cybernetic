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

    public int currentHealth = 100;
    public int maxHealth = 100;

    public int currentLevel = 1;
    public int maxLevel = 1;
}
