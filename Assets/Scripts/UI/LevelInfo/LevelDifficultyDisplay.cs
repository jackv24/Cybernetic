using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelDifficultyDisplay : MonoBehaviour
{
    //Full star sprite (images should by default be an empty star
    public Sprite fullStar;

    void Start()
    {
        //If there is level info to read from
        if (GameManager.levelInfo)
        {
            //Iterate through required number of stars
            for (int i = 0; i < GameManager.levelInfo.difficulty; i++)
            {
                //Set their sprites to full
                transform.GetChild(i).GetComponent<Image>().sprite = fullStar;
            }
        }
    }
}
