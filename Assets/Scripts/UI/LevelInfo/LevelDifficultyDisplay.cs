using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelDifficultyDisplay : MonoBehaviour
{
    public Sprite fullStar;

    void Start()
    {
        if (GameManager.levelInfo)
        {
            for (int i = 0; i < GameManager.levelInfo.difficulty; i++)
            {
                transform.GetChild(i).GetComponent<Image>().sprite = fullStar;
            }
        }
    }
}
