using UnityEngine;
using System.Collections;

public class LevelPreviewGenerator : MonoBehaviour
{
    //prefabs for sprites to represent different nodes
    public GameObject blankSquare;

    // A calculated value for the size of the squares
    private int squareSize = 0;

    //A reference to the level generator
    private LevelGenerator levelGenerator;

    //A reference to the rect transform on this gameobject
    private RectTransform rectTransform;
    //map size gained from rect transform's width
    private int mapSize = 0;

    void Start()
    {
        //Get a reference to the rect transform on this gameobject
        rectTransform = GetComponent<RectTransform>();

        //Make sure the rect is a perfect square
        if (rectTransform.sizeDelta.x != rectTransform.sizeDelta.y)
            Debug.LogWarning("Level Preview height and width are not equal!");

        //Get a reference to the level generator on this gameobject
        levelGenerator = GetComponent<LevelGenerator>();

        //Get map size from rect's width (arbitrary - height would work too)
        mapSize = (int)rectTransform.sizeDelta.x;
        //Calculate the size of each square
        squareSize = mapSize / levelGenerator.levelSize;

        //Perform initial update of grid (run after generating level)
        UpdateGrid();
    }

    //Updates the displayed grid
    public void UpdateGrid()
    {
        //Get array of nodes from level generator
        GameObject[,] gridNodes = levelGenerator.gridNodes;

        //Iterate through the 2D array
        for (int x = 0; x < levelGenerator.levelSize; x++)
        {
            for (int y = 0; y < levelGenerator.levelSize; y++)
            {
                GameObject square = null;

                //Determine what sqaure to instantaite based on the already generated grid
                if (gridNodes[x, y] == null)
                    square = blankSquare;

                //Instantiate suare graphic
                GameObject obj = Instantiate(square);
                //Set parent to this gameobject
                obj.transform.SetParent(transform, false);

                //Set position based on array index
                Vector3 pos = new Vector3(x * squareSize, y * squareSize, 0);
                obj.GetComponent<RectTransform>().localPosition = pos;

                //Set name based on index (for organisation)
                obj.name = string.Format("Node ({0}, {1})", x, y);
            }
        }
    }
}
