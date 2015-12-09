using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelInfoDisplay : MonoBehaviour
{
    //Displays the basic level info (number of rounds)
    public Text info;

    //Display text for rounds, enemies and speed
    public Text rounds;
    public Text enemies;
    public Text spawnRate;

    void Start()
    {
        //If there is level information to load
        if (GameManager.levelInfo)
        {
            //Set the number of rounds
            info.text = "Rounds: " + GameManager.levelInfo.rounds.Count;

            //Set the initial column text, ending with a newline
            rounds.text = "Round\n";
            enemies.text = "Enemies\n";
            spawnRate.text = "Speed\n";

            //Iterate through all round array items
            for (int i = 0; i < GameManager.levelInfo.rounds.Count; i++)
            {
                //Display current round
                rounds.text += i + 1 + "\n";
                //Display amount of enemies for that round
                enemies.text += string.Format("{0} <size={2}>({1})</size>\n",
                    GameManager.levelInfo.rounds[i].enemies,
                    GameManager.levelInfo.rounds[i].enemySpawnLevel,
                    enemies.fontSize / 1.5);
                //Display the speed multipler of those enemies
                spawnRate.text += string.Format("x{0:0.0}\n", GameManager.levelInfo.rounds[i].spawnRate);
            }
        }
    }
}
