using UnityEngine;
using System.Collections;

public class TooltipFloat : MonoBehaviour
{
    public bool horizontal = false;
    public bool vertical = false;

    public bool scaleByDistance = false;
    public float scale = 0.1f;

    private Vector3 initialScale;

    void Start()
    {
        initialScale = transform.localScale;
    }

    void LateUpdate()
    {
        if (vertical)
            transform.eulerAngles = new Vector3(0, Camera.main.transform.rotation.eulerAngles.y, 0);

        if (horizontal)
            transform.eulerAngles = new Vector3(Camera.main.transform.rotation.eulerAngles.x, transform.eulerAngles.y, 0);

        if (scaleByDistance)
        {
            float dist = Vector3.Distance(Camera.main.transform.position, transform.position);

            transform.localScale = initialScale * dist * scale;
        }
    }
}
