using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RoundStatsDisplay : MonoBehaviour
{
    public Slider statsSlider;
    public Text statsText;

    public GameObject skipButton;

    void Update()
    {
        if (GameManager.roundManager.isDefendRound)
        {
            skipButton.SetActive(false);

            statsSlider.value =(float)GameManager.roundManager.enemies / GameManager.roundManager.rounds[GameManager.roundManager.currentRound].enemies;

            statsText.text = "Enemies: " + GameManager.roundManager.enemies + "/" + GameManager.roundManager.rounds[GameManager.roundManager.currentRound].enemies;
        }
        else
        {
            skipButton.SetActive(true);

            statsSlider.value = GameManager.roundManager.currentBuildTime / GameManager.roundManager.buildTime;

            statsText.text = "Time Left: " + GameManager.roundManager.currentBuildTime + "s";
        }
    }

    public void SkipBuild()
    {
        GameManager.roundManager.currentBuildTime = 0;
    }
}
