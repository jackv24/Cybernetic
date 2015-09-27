using UnityEngine;
using System.Collections;

public class TowerAppearance : MonoBehaviour
{
    public MeshRenderer[] meshRenderers;

    public void ChangeMaterial(Material material)
    {
        foreach (MeshRenderer mesh in meshRenderers)
        {
            mesh.material = material;
        }
    }
}
