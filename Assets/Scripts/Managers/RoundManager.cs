using UnityEngine;
using System.Collections;

public class RoundManager : MonoBehaviour
{
    public int rounds;
    public int currentRound;

    public bool isBuildRound = true;
    public float buildTime = 60f;

    void Start()
    {
        if (currentRound < rounds)
            StartNextRound();
    }

    public void StartNextRound()
    {
        currentRound++;

        StartBuild();
    }

    void StartBuild()
    {
        isBuildRound = true;
    }

    void StartWave()
    {
        isBuildRound = false;
    }
}
