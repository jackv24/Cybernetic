using UnityEngine;
using System.Collections;

public class LevelInfo : MonoBehaviour
{
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

    //How fast will the enemies move (speed multiplier)
    public float speed = 1f;
}