using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
    //The level, as a 2D array
    private LevelNode[,] levelNodes;

    //Prefabs to instantiate for the level
    public GameObject buildNode;
    public GameObject pathNodeStraight;
    public GameObject pathNodeBend;

    public GameObject homeBase;
    public GameObject spawner;

    void Start()
    {
        //Make new gameobject to parent level prefabs to
        GameObject level = new GameObject("Level");

        //Get level data from the level generator
        levelNodes = LevelGenerator.instance.gridNodes;

        int levelSize = LevelGenerator.instance.levelSize;

        for (int x = 0; x < levelSize; x++)
        {
            for (int y = 0; y < levelSize; y++)
            {
                //If node is a path, place path and orient accordingly
                if (levelNodes[x, y].nodeType == LevelNode.Type.Path)
                {
                    GameObject path;

                    if (levelNodes[x, y].subtype == "vertical" || levelNodes[x, y].subtype == "horizontal")
                    {
                        path = Instantiate(pathNodeStraight, new Vector3(x, 0, y), Quaternion.identity) as GameObject;
                        path.transform.parent = level.transform;
                    }
                    else
                    {
                        path = Instantiate(pathNodeBend, new Vector3(x, 0, y), Quaternion.identity) as GameObject;
                        path.transform.parent = level.transform;
                    }

                    if (levelNodes[x, y].subtype == "horizontal")
                        path.transform.eulerAngles = new Vector3(0, 90, 0);

                    else if(levelNodes[x, y].subtype == "bottomleft")
                        path.transform.eulerAngles = new Vector3(0, 90, 0);
                    else if (levelNodes[x, y].subtype == "topleft")
                        path.transform.eulerAngles = new Vector3(0, 180, 0);
                    else if (levelNodes[x, y].subtype == "topright")
                        path.transform.eulerAngles = new Vector3(0, 270, 0);
                }
                else if (levelNodes[x, y].nodeType == LevelNode.Type.Build)
                {
                    GameObject build = Instantiate(buildNode, new Vector3(x, 0, y), Quaternion.identity) as GameObject;
                    build.transform.parent = level.transform;
                }
                else if (levelNodes[x, y].nodeType == LevelNode.Type.Base)
                {
                    GameObject baseG = Instantiate(homeBase, new Vector3(x, 0, y) + homeBase.transform.position, homeBase.transform.rotation) as GameObject;
                    baseG.transform.parent = level.transform;
                }
                else if (levelNodes[x, y].nodeType == LevelNode.Type.Spawner)
                {
                    GameObject spawn = Instantiate(spawner, new Vector3(x, 0, y) + spawner.transform.position, Quaternion.identity) as GameObject;
                    spawn.transform.parent = level.transform;

                    spawn.GetComponent<Spawner>().nextNode = levelNodes[x, y].nextNode;
                }
            }
        }
    }
}
