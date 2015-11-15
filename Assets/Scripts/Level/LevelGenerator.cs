using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour
{
    // A 2D array of the nodes in the level
    public GameObject[,] gridNodes;

    //The horizontal and vertical size of the (assumedly square) level
    public int levelSize = 16;

    void Start()
    {
        //Initialise the array
        gridNodes = new GameObject[levelSize, levelSize];

        //Generate level
        Regenerate();
    }

    public void Regenerate()
    {
        //For each row in the level...
        for (int x = 0; x < levelSize; x++)
        {
            //...generate a column
            for (int y = 0; y < levelSize; y++)
            {
                gridNodes[x, y] = null;
            }
        }
    }
}
