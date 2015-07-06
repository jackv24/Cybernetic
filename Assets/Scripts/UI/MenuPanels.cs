using UnityEngine;
using System.Collections;

public class MenuPanels : MonoBehaviour
{
    public GameObject[] menuPanels;

    public int selected = 0;

    void Start()
    {
        for (int i = 0; i < menuPanels.Length; i++)
        {
            menuPanels[i].SetActive(false);
        }

        menuPanels[selected].SetActive(true);
    }

    public void OpenPanel(int index)
    {
        menuPanels[selected].SetActive(false);

        menuPanels[index].SetActive(true);

        selected = index;
    }
}
