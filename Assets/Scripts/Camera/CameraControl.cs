using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    //The rate at which the camera will track along the ground
    public float moveSpeed = 20.0f;

    //The current distance from the target
    public float CameraDistance = 6.0f;
    //Minimum distance from the target
    public float CameraDistanceMin = 1.0f;
    //Maximum distance from the target
    public float CameraDistanceMax = 10.0f;

    //Current Height of the camera relative to the target
    public float CameraHeight = 1.5f;
    //Camera orbit sensitivity
    public float CameraSensitivity = 150.0f;

    //Current camera pitch (local x rotation)
    public float CameraPitch = 0.0f;
    //Minimum camera pitch
    public float CameraPitchMin = -5.0f;
    //Maximum camera putch
    public float CameraPitchMax = 60.0f;

    //The rate at which the camera zooms in and out
    public float CameraZoomSpeed = 10.0f;

    //The camera target
    public Transform target;

    void Start()
    {
        //If there is no target, create one.
        if (!target)
            target = new GameObject("Target").transform;

        //If a levelcenter gameobject exists
        if (GameObject.Find("LevelCenter"))
        {
            Transform center = GameObject.Find("LevelCenter").transform;
            //Position the target at the centre of the level
            target.position = center.position;
        }
    }

    //LateUpdate is used to make sure the camera moves during the same frame as the target, instead of the frame after
    void LateUpdate()
    {
        //If the game hasn't started, dont execute the rest of the code
        if (GameManager.startGame)
        {
            if (Input.GetButton("Move Camera"))
            {
                target.eulerAngles = new Vector3(target.eulerAngles.x, transform.eulerAngles.y, target.eulerAngles.z);

                target.Translate(new Vector3(-Input.GetAxis("Mouse X"), 0, -Input.GetAxis("Mouse Y")) * moveSpeed * Time.deltaTime);
            }

            //Don't do anything if no target is registered.
            if (target == null)
                return;

            if (Input.GetButton("Right Click"))
                OrbitCamera(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));

            //Zoom
            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
                CameraDistance -= CameraZoomSpeed * Time.deltaTime;
            else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
                CameraDistance += CameraZoomSpeed * Time.deltaTime;

            CameraDistance = Mathf.Clamp(CameraDistance, CameraDistanceMin, CameraDistanceMax);
        }

        transform.position = new Vector3(target.position.x, target.position.y, target.position.z);
        transform.position += transform.rotation * Vector3.back * CameraDistance;
        transform.position += Vector3.up * CameraHeight;
    }

    //Function seperated for cameraorbit script
    public void OrbitCamera(Vector2 input)
    {
        //Move camera up/down
        CameraPitch -= input.y * CameraSensitivity * Time.deltaTime;
        CameraPitch = Mathf.Clamp(CameraPitch, CameraPitchMin, CameraPitchMax);

        //Move camera left/right
        transform.localEulerAngles = new Vector3(CameraPitch, transform.localEulerAngles.y + input.x * CameraSensitivity * Time.deltaTime, 0);
        transform.localEulerAngles = new Vector3(CameraPitch, transform.localEulerAngles.y, 0);
    }
}