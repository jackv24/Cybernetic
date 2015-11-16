﻿using UnityEngine;
using System.Collections;

public class LevelNode
{
    public enum Type
    {
        Blank,
        Path,
        Build,
        Base,
        Spawner
    }

    //What type of node this node is
    public Type nodeType = Type.Blank;

    public int x = 0, y = 0;

    //Constructor
    public LevelNode(int xIndex, int yIndex, Type type)
    {
        x = xIndex;
        y = yIndex;

        nodeType = type;
    }
}
