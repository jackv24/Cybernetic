using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    // A 2D array of the nodes in the level
    public LevelNode[,] gridNodes;

    //Nodes from which more nodes can be generated
    private List<LevelNode> activeNodes;

    //The horizontal and vertical size of the (assumedly square) level
    public int levelSize = 16;

    //Amount of paths nodes to begin generation
    private int initialPaths = 3;

    void Start()
    {
        //Initialise the array
        gridNodes = new LevelNode[levelSize, levelSize];

        activeNodes = new List<LevelNode>();

        //Generate level
        Regenerate();
    }

    //Loads preferences set by GUI
    void LoadPrefs()
    {
        initialPaths = PlayerPrefs.GetInt("initialPaths", 1);
    }

    public void Regenerate()
    {
        LoadPrefs();

        //Generate a grid of blank nodes
        for (int x = 0; x < levelSize; x++)
            for (int y = 0; y < levelSize; y++)
                gridNodes[x, y] = new LevelNode(x, y, LevelNode.Type.Blank);

        //Start generating level
        Initialise();
        GeneratePaths();
    }

    void Initialise()
    {
        //Arbitrarily chosen to have a buffer of one quarter of map size
        int quarterSize = levelSize / 4;

        //Choose a random position on the map
        int x = Random.Range(0, levelSize + 1);
        int y = Random.Range(0, levelSize + 1);

        //Ensure this position is within the bounds of the buffer
        if (x > quarterSize)
            x -= quarterSize;
        if (x < quarterSize)
            x += quarterSize;
        if (y > quarterSize)
            y -= quarterSize;
        if (y < quarterSize)
            y += quarterSize;

        //place the base at this random position
        gridNodes[x, y].nodeType = LevelNode.Type.Base;

        //Booleans for available directions
        bool up = true, down = true, left = true, right = true;

        //Generate initial paths
        for (int i = 0; i < initialPaths; i++)
        {
            Vector2 direction = GetRandomDirection(down, right, up, left);
            int dirX = (int)direction.x, dirY = (int)direction.y;

            //Set node as path
            gridNodes[x + dirX, y + dirY].nodeType = LevelNode.Type.Path;

            //Make sure this direction is not chosen again
            if (direction == new Vector2(0, -1))
                down = false;
            if (direction == new Vector2(1, 0))
                right = false;
            if (direction == new Vector2(0, 1))
                up = false;
            if (direction == new Vector2(-1, 0))
                left = false;

            //Add path to list of active nodes
            activeNodes.Add(gridNodes[x + dirX, y + dirY]);
        }
    }

    void GeneratePaths()
    {
        foreach (LevelNode node in activeNodes)
        {
            
        }
    }

    //returns a random direction out of those specified
    Vector2 GetRandomDirection(bool down, bool right, bool up, bool left)
    {
        //Initialise variables.
        Vector2 direction = Vector2.zero;
        int dir = 0;

        //Direction from 0 - 3 is down, right, up, left
        dir = Random.Range(0, 4);

        //If the direction is available, return it, otherwise generate a new direction
        switch (dir)
        {
            case 0:
                if (down)
                    direction = new Vector2(0, -1);
                else
                    direction = GetRandomDirection(down, right, up, left);
                break;
            case 1:
                if(right)
                    direction = new Vector2(1, 0);
                else
                    direction = GetRandomDirection(down, right, up, left);
                break;
            case 2:
                if(up)
                    direction = new Vector2(0, 1);
                else
                    direction = GetRandomDirection(down, right, up, left);
                break;
            case 3:
                if(left)
                    direction = new Vector2(-1, 0);
                else
                    direction = GetRandomDirection(down, right, up, left);
                break;
        }
        

        return direction;
    }
}
