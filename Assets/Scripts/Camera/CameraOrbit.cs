using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CameraControl))]
public class CameraOrbit : MonoBehaviour
{
    //The speed at which the camera will orbit
    public float speed = 10f;

    //Reference to camera control script
    private CameraControl cam;

    void Start()
    {
        cam = GetComponent<CameraControl>();
    }

    void Update()
    {
        //If the game has not started
        if (!GameManager.startGame)
        {
            Vector2 orbitVector = Vector2.left * speed;

            //Oribit the camera
            cam.OrbitCamera(orbitVector * Time.deltaTime);
        }
    }
}
