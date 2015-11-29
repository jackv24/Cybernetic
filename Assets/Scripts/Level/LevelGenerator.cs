using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator instance = null;

    // A 2D array of the nodes in the level
    public LevelNode[,] gridNodes;

    //Nodes from which more nodes can be generated
    private List<LevelNode> activeNodes;

    private List<LevelNode> pathNodes;

    //The horizontal and vertical size of the (assumedly square) level
    public int levelSize = 16;

    //Amount of paths nodes to begin generation
    private int initialPaths = 1;
    private float turnProb = 0.5f;
    private float buildFill = 0.8f;

    void Awake()
    {
        //Ensures that there is only ever one SoundManager
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        //Initialise the array
        gridNodes = new LevelNode[levelSize, levelSize];

        activeNodes = new List<LevelNode>();
        pathNodes = new List<LevelNode>();

        Generate();
    }

    //Loads preferences set by GUI
    void LoadPrefs()
    {
        initialPaths = (int)PlayerPrefs.GetFloat("initialPaths", 1);
        turnProb = PlayerPrefs.GetFloat("turnProb", 0.5f);
        buildFill = PlayerPrefs.GetFloat("buildFill", 0.8f);
    }

    public void Generate()
    {
        LoadPrefs();

        //Generate a grid of blank nodes
        for (int x = 0; x < levelSize; x++)
            for (int y = 0; y < levelSize; y++)
                gridNodes[x, y] = new LevelNode(x, y, LevelNode.Type.Blank);

        //Start generating level
        Initialise();
        GeneratePaths();
        GenerateBuildAreas();
    }

    void Initialise()
    {
        activeNodes.Clear();
        pathNodes.Clear();

        //Arbitrarily chosen to have a buffer of one quarter of map size
        int bufferSize = levelSize / 3;

        //Choose a random position on the map
        int x = Random.Range(0, levelSize + 1);
        int y = Random.Range(0, levelSize + 1);

        //Ensure this position is within the bounds of the buffer
        if (x > bufferSize)
            x -= bufferSize;
        if (x < bufferSize)
            x += bufferSize;
        if (y > bufferSize)
            y -= bufferSize;
        if (y < bufferSize)
            y += bufferSize;

        //place the base at this random position
        gridNodes[x, y].nodeType = LevelNode.Type.Base;

        //Booleans for available directions
        bool up = true, down = true, left = true, right = true;

        //Generate initial paths
        for (int i = 0; i < initialPaths; i++)
        {
            Vector2 direction = GetRandomDirection(down, right, up, left);
            int posX = x + (int)direction.x;
            int posY = y + (int)direction.y;

            //Set node as path
            gridNodes[posX, posY].nodeType = LevelNode.Type.Path;

            gridNodes[posX, posY].direction = direction;

            if (direction.x != 0)
                gridNodes[posX, posY].subtype = "horizontal";

            gridNodes[posX, posY].nextNode = gridNodes[x, y];

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
            activeNodes.Add(gridNodes[posX, posY]);

            pathNodes.Add(gridNodes[posX, posY]);
        }
    }

    void GeneratePaths()
    {
        bool running = true;

        //Nodes which have just been spawned
        List<LevelNode> currentNodes = new List<LevelNode>();

        //Loops until all paths have ended
        while (running)
        {
            //Every active node (at the end of a path) attempts to spawn another node
            foreach (LevelNode node in activeNodes)
            {
                bool done = false;

                //Booleans for available directions
                bool up = true, down = true, left = true, right = true;

                //Loops until blank node is found, or no blank node is found (path ends)
                while (!done)
                {
                    //Unit vector for direction
                    Vector2 direction;

                    //Turn probability determines whether a random direction is chosen or if it continues in a straight line
                    if (Random.Range(0, 100) < turnProb * 100)
                        direction = GetRandomDirection(down, right, up, left);
                    else
                        direction = node.direction;

                    //New node positions
                    int posX = node.x + (int)direction.x;
                    int posY = node.y + (int)direction.y;

                    //if this new node is not within map bounds, end path
                    if (CheckIsWithinMapBounds(posX, posY) == false)
                        down = right = up = left = false;
                    //otherwise, if the node is clear, make it a path
                    else if (CheckNodeIsType(posX, posY, LevelNode.Type.Blank))
                    {
                        done = true;

                        //Add to current nodes to continue paths later
                        currentNodes.Add(gridNodes[posX, posY]);

                        pathNodes.Add(gridNodes[posX, posY]);

                        //Set as path
                        gridNodes[posX, posY].nodeType = LevelNode.Type.Path;
                        //Remember direction from last node (for straight paths)
                        gridNodes[posX, posY].direction = direction;

                        gridNodes[posX, posY].nextNode = node;

                        //Determine what path orientation to display
                        if (direction.x != 0)
                            gridNodes[posX, posY].subtype = "horizontal";

                        if (gridNodes[posX, posY].direction != node.direction)
                        {
                            //Check all possible corner directions
                            if (node.direction == new Vector2(1, 0) && gridNodes[posX, posY].direction == new Vector2(0, 1))
                                node.subtype = "bottomright"; //Set previous node to face into this one
                            else if (node.direction == new Vector2(-1, 0) && gridNodes[posX, posY].direction == new Vector2(0, 1))
                                node.subtype = "bottomleft";
                            else if (node.direction == new Vector2(0, 1) && gridNodes[posX, posY].direction == new Vector2(1, 0))
                                node.subtype = "topleft";
                            else if (node.direction == new Vector2(0, -1) && gridNodes[posX, posY].direction == new Vector2(1, 0))
                                node.subtype = "bottomleft";

                            else if (node.direction == new Vector2(1, 0) && gridNodes[posX, posY].direction == new Vector2(0, -1))
                                node.subtype = "topright";
                            else if (node.direction == new Vector2(-1, 0) && gridNodes[posX, posY].direction == new Vector2(0, -1))
                                node.subtype = "topleft";
                            else if (node.direction == new Vector2(0, 1) && gridNodes[posX, posY].direction == new Vector2(-1, 0))
                                node.subtype = "topright";
                            else if (node.direction == new Vector2(0, -1) && gridNodes[posX, posY].direction == new Vector2(-1, 0))
                                node.subtype = "bottomright";

                        }
                    }

                    //Make sure this direction is not chosen again
                    if (direction == new Vector2(0, -1))
                        down = false;
                    if (direction == new Vector2(1, 0))
                        right = false;
                    if (direction == new Vector2(0, 1))
                        up = false;
                    if (direction == new Vector2(-1, 0))
                        left = false;

                    //If there are no available spaces, end the path with a spawner
                    if (!down && !right && !up && !left)
                    {
                        done = true;

                        node.nodeType = LevelNode.Type.Spawner;
                        pathNodes.Remove(node);
                    }
                }
            }

            //All active nodes have attempted to spawn more paths, so clear the list
            activeNodes.Clear();
            //Fill active nodes list with those paths spawned in the last run
            activeNodes = new List<LevelNode>(currentNodes);
            //Clear current nodes
            currentNodes.Clear();

            //If there are no more active nodes, stop running generator
            if (activeNodes.Count == 0)
                running = false;
        }
    }

    void GenerateBuildAreas()
    {
        foreach (LevelNode node in pathNodes)
        {
            bool done = false;

            bool down = true, right = true, up = true, left = true;

            while (!done)
            {
                Vector2 direction = GetRandomDirection(down, right, up, left);

                //New node positions
                int posX = node.x + (int)direction.x;
                int posY = node.y + (int)direction.y;

                //if this new node is not within map bounds, ignore it
                if (CheckIsWithinMapBounds(posX, posY) == false)
                    down = right = up = left = false;
                //otherwise, if the node is clear, make it a build area
                else if (CheckNodeIsType(posX, posY, LevelNode.Type.Blank))
                {
                    if (Random.Range(0, 100) < buildFill * 100)
                        gridNodes[posX, posY].nodeType = LevelNode.Type.Build;
                }

                //Make sure this direction is not chosen again
                if (direction == new Vector2(0, -1))
                    down = false;
                if (direction == new Vector2(1, 0))
                    right = false;
                if (direction == new Vector2(0, 1))
                    up = false;
                if (direction == new Vector2(-1, 0))
                    left = false;

                if (!down && !right && !up && !left)
                {
                    done = true;
                }
            }
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

    //Checks if node is within map bounds
    bool CheckIsWithinMapBounds(int xIndex, int yIndex)
    {
        if (xIndex >= 0 && xIndex < levelSize & yIndex >= 0 && yIndex < levelSize)
        {
            return true;
        }

        return false;
    }

    //Checks if the node is clear
    bool CheckNodeIsType(int xIndex, int yIndex, LevelNode.Type type)
    {
        if (CheckIsWithinMapBounds(xIndex, yIndex))
        {
            if (gridNodes[xIndex, yIndex].nodeType == type)
            {
                return true;
            }
        }

        return false;
    }
}
