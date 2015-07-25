using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RoundInfoDisplay : MonoBehaviour
{
    private Text infoText;
    private string initialInfoText;

    void Start()
    {
        infoText = GetComponent<Text>();
        initialInfoText = infoText.text;
    }

    void Update()
    {
        string roundText = GameManager.roundManager.currentRound + 1 + "/" + GameManager.roundManager.rounds.Length;

        //If this is a defend round
        if (GameManager.roundManager.isDefendRound)
            infoText.text = string.Format(initialInfoText, roundText, "Defend");
        //If this is a build round
        else
            infoText.text = string.Format(initialInfoText, roundText, "Build");
    }
}
