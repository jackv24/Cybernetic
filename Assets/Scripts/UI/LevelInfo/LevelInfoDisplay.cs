using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelInfoDisplay : MonoBehaviour
{
    public Text titleText;

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
            titleText.text = string.Format(titleText.text, GameManager.levelInfo.world, GameManager.levelInfo.subtitle, GameManager.levelInfo.level);

            //Set the number of rounds
            info.text = "Rounds: " + GameManager.levelInfo.rounds.Length;

            //Set the initial column text, ending with a newline
            rounds.text = "Round\n";
            enemies.text = "Enemies\n";
            spawnRate.text = "Speed\n";

            //Iterate through all round array items
            for (int i = 0; i < GameManager.levelInfo.rounds.Length; i++)
            {
                //Display current round
                rounds.text += i + 1 + "\n";
                //Display amount of enemies for that round
                enemies.text += GameManager.levelInfo.rounds[i].enemies + "\n";
                //Display the speed multipler of those enemies
                spawnRate.text += "x" + GameManager.levelInfo.rounds[i].spawnRate + "\n";
            }
        }
    }
}
