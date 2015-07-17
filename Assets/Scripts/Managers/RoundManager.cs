using UnityEngine;
using System.Collections;

public class RoundManager : MonoBehaviour
{
    //Time for build round
    public float buildTime = 10f;
    //How much time is left in the current build round
    public float currentBuildTime = 0;

    //How many enemies are left in the current defend round
    public int enemies = 0;

    //An array of rounds in the level
    public Round[] rounds;
    //The current round
    public int currentRound = -1;

    //Is the round in the defend state
    public bool isDefendRound = false;

    private bool startGame = true;

    void Start()
    {
        //Gets round array from level info
        rounds = GameManager.levelInfo.rounds;
    }

    void Update()
    {
        //Makes sure the code is only run once
        if (startGame && GameManager.startGame)
        {
            startGame = false;

            //Starts the first round
            StartNextRound();
        }

        //If the round is in the defend state
        if (isDefendRound)
        {
            enemies = GameManager.enemyManager.enemiesInRound;

            //And all enemies are killed
            if (enemies <= 0)
            {
                isDefendRound = false;

                //Start the next round
                StartNextRound();
            }
        }
    }

    //Starts the next round
    void StartNextRound()
    {
        currentRound++;

        //If there are rounds left
        if (currentRound < rounds.Length)
        {
            StartCoroutine("BuildRoundTimer");
        }
        else
            Debug.Log("Level Complete");
    }

    IEnumerator BuildRoundTimer()
    {
        currentBuildTime = buildTime;

        while (currentBuildTime > 0)
        {
            yield return new WaitForSeconds(1);

            currentBuildTime--;
        }

        currentBuildTime = 0;

        StartDefendRound();
    }

    void StartDefendRound()
    {
        enemies = rounds[currentRound].enemies;

        GameManager.enemyManager.StartSpawn(enemies, 1f);

        isDefendRound = true;
    }
}
