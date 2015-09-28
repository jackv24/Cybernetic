using UnityEngine;
using System.Collections;

public class LevelInfo : MonoBehaviour
{
    public int world = 1;
    public string subtitle = "The Basics";

    public int level = 1;

    //An array of rounds in the level
    public Round[] rounds;

    //Level difficulty (from 1-5)
    [Range(1, 5)]
    public int difficulty = 1;

    void Start()
    {
        //Set reference to this script in the game manager
        GameManager.levelInfo = this;
    }
}

//Class to hold round info (serializable to appear in inspector
[System.Serializable]
public class Round
{
    //How many enemies in this round?
    public int enemies = 5;

    //How fast enemies spawn this round
    public float spawnRate = 1f;

    public GameObject[] enemiesToSpawn;
}