using UnityEngine;
using System.Collections;

public class TowerTooltip : MonoBehaviour
{
    public Node selectedNode;

    [HideInInspector]
    public SelectTowers selectTowers;

    public void ClearNode()
    {
        selectedNode.Clear();

        selectTowers.RemoveTooltip();
    }
}
