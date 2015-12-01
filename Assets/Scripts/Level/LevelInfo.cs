using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelInfo : MonoBehaviour
{
    //An array of rounds in the level
    public List<Round> rounds;

    private int roundCount = 0;

    private float enemySpawnRate = 0;

    void Start()
    {
        rounds = new List<Round>();

        roundCount = int.Parse(PlayerPrefs.GetString("roundCount", "10"));

        enemySpawnRate = PlayerPrefs.GetFloat("enemySpawnRate", 1f);

        //Set reference to this script in the game manager
        GameManager.levelInfo = this;

        GenerateRounds();
    }

    void GenerateRounds()
    {
        for (int i = 0; i < roundCount; i++)
        {
            Round round = new Round();

            round.enemies = Mathf.RoundToInt(6 * Mathf.Sqrt(i + 2 * i + 1));
            round.spawnRate = enemySpawnRate * Mathf.Sqrt(i + 0.5f);

            rounds.Add(round);
        }
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

    public int enemySpawnLevel = 1;
}