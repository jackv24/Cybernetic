using UnityEngine;
using System.Collections;

//A scrollmenu, which scrolls between menu items. Horizontal only.
public class ScrollMenu : MonoBehaviour
{
    //Store an array of all menu items
    public Transform[] menuItems;

    //The amount of pixels to move the panel
    public float offset = 360f;

    //The speed at which the panel will move
    [Range(0, 1)]
    public float speed = 0.25f;

    //The first item to be selected
    public int firstItem = 1;

    //The current item selected
    private int currentItem = 0;

    void Start()
    {
        //If there is more than one menu item, set preferred start item
        if (menuItems.Length > 1)
            currentItem = firstItem;
    }

    void Update()
    {
        int initialItem = currentItem;

        if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
        {
            if (currentItem < menuItems.Length - 1)
                currentItem++;
        }

        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
        {
            if (currentItem > 0)
                currentItem--;
        }

        if (currentItem != initialItem)
        {
            menuItems[initialItem].localScale = Vector3.one * 0.75f;
        }
        else
        {
            menuItems[initialItem].localScale = Vector3.one;
        }

        Vector3 pos = transform.position;
        pos.x = Mathf.Lerp(pos.x, pos.x = (-offset * currentItem) + (Screen.width / 2), speed);
        transform.position = pos;
    }
}
