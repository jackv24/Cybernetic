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

                GameManager.gameManager.buildMenu.SetActive(true);

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
            GameManager.gameManager.EndGame(true);
    }

    //A coroutine to time the length of the build round
    IEnumerator BuildRoundTimer()
    {
        //Starts the current build time value
        currentBuildTime = buildTime;

        //While there is still time left in the round
        while (currentBuildTime > 0)
        {
            //Wait one second, then...
            yield return new WaitForSeconds(1);
            //decrement the amount of time left
            currentBuildTime--;
        }

        //Make certain that build time ends on 0
        currentBuildTime = 0;

        //Start the defend round
        StartDefendRound();
    }

    //Starts the defend round
    void StartDefendRound()
    {
        //Gets the amount of enemies to spawn this round
        enemies = rounds[currentRound].enemies;
        //Tells the enemy manager to spawn the correct amount of enemies
        GameManager.enemyManager.StartSpawn(enemies, rounds[currentRound].spawnRate);
        GameManager.enemySpeed = rounds[currentRound].spawnRate;

        //Updates the current round bool value
        isDefendRound = true;

        GameManager.gameManager.buildMenu.SetActive(false);
    }
}
