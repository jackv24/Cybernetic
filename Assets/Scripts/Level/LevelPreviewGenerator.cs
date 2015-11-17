using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelPreviewGenerator : MonoBehaviour
{
    //prefabs for sprites to represent different nodes
    public GameObject blankSquare;
    public GameObject buildSquare;
    public GameObject baseSquare;
    public GameObject spawnSquare;

    public GameObject pathSquareVertical;
    public GameObject pathSquareHorizontal;
    public GameObject pathSquareTopLeft;
    public GameObject pathSquareTopRight;
    public GameObject pathSquareBottomLeft;
    public GameObject pathSquareBottomRight;

    // A calculated value for the size of the squares
    private int squareSize = 0;

    //A reference to the level generator
    private LevelGenerator levelGenerator;

    //A reference to the rect transform on this gameobject
    private RectTransform rectTransform;
    //map size gained from rect transform's width
    private int mapSize = 0;

    private List<GameObject> nodeSquares;

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

        nodeSquares = new List<GameObject>();

        //Perform initial update of grid (run after generating level)
        UpdateGrid();
    }

    //Updates the displayed grid
    public void UpdateGrid()
    {
        //Destroy all current squares and remove from list
        foreach (GameObject node in nodeSquares)
            Destroy(node);
        nodeSquares.RemoveAll(delegate (GameObject o) { return o == null; });

        //Get array of nodes from level generator
        LevelNode[,] gridNodes = levelGenerator.gridNodes;

        //Iterate through the 2D array
        for (int x = 0; x < levelGenerator.levelSize; x++)
        {
            for (int y = 0; y < levelGenerator.levelSize; y++)
            {
                GameObject square = null;

                //Determine what square to instantiate based on the already generated grid
                switch (gridNodes[x, y].nodeType)
                {
                    case LevelNode.Type.Blank:
                        square = blankSquare;
                        break;
                    case LevelNode.Type.Path:
                        if (gridNodes[x, y].subtype == "vertical")
                            square = pathSquareVertical;
                        else if (gridNodes[x, y].subtype == "horizontal")
                            square = pathSquareHorizontal;
                        else if (gridNodes[x, y].subtype == "topleft")
                            square = pathSquareTopLeft;
                        else if (gridNodes[x, y].subtype == "topright")
                            square = pathSquareTopRight;
                        else if (gridNodes[x, y].subtype == "bottomleft")
                            square = pathSquareBottomLeft;
                        else if (gridNodes[x, y].subtype == "bottomright")
                            square = pathSquareBottomRight;
                        break;
                    case LevelNode.Type.Build:
                        square = buildSquare;
                        break;
                    case LevelNode.Type.Base:
                        square = baseSquare;
                        break;
                    case LevelNode.Type.Spawner:
                        square = spawnSquare;
                        break;
                }

                //Instantiate suare graphic
                GameObject obj = Instantiate(square);
                //Set parent to this gameobject
                obj.transform.SetParent(transform, false);

                //Set position based on array index
                Vector3 pos = new Vector3(x * squareSize, y * squareSize, 0);
                obj.GetComponent<RectTransform>().localPosition = pos;

                //Set name based on index (for organisation)
                obj.name = string.Format("Node ({0}, {1})", x, y);

                //Add square to list for destruction on regenerate
                nodeSquares.Add(obj);
            }
        }
    }
}
