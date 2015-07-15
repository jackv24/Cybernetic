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
    public Text speed;

    void Start()
    {
        if (GameManager.levelInfo)
        {
            info.text = "Rounds: " + GameManager.levelInfo.rounds.Length;

            rounds.text = "Round\n";
            enemies.text = "Enemies\n";
            speed.text = "Speed\n";

            for (int i = 0; i < GameManager.levelInfo.rounds.Length; i++)
            {
                rounds.text += i + 1 + "\n";

                enemies.text += GameManager.levelInfo.rounds[i].enemies + "\n";

                speed.text += "x" + GameManager.levelInfo.rounds[i].speed + "\n";
            }
        }
    }
}
