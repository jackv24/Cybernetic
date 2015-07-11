using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
    public int world = 1;
    public int level = 1;

    void Awake()
    {
        world = PlayerPrefs.GetInt("world", 1);
        level = PlayerPrefs.GetInt("level", 1);
    }

    void Start()
    {
        if (transform.childCount == 1)
            transform.GetChild(0).gameObject.SetActive(false);

        GameObject levelPrefab = Resources.Load<GameObject>("Levels/World " + world + "/Level " + level);

        if (levelPrefab)
        {
            GameObject levelObj = Instantiate(levelPrefab, levelPrefab.transform.position, levelPrefab.transform.rotation) as GameObject;
            levelObj.transform.parent = transform;

            GameManager.levelLoaded = true;
        }
    }
}
