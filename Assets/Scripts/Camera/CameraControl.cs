using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    public Transform target;

    public float moveSpeed = 20.0f;

    public float CameraDistance = 6.0f;
    public float CameraDistanceMin = 1.0f;
    public float CameraDistanceMax = 10.0f;

    public float CameraHeight = 1.5f;
    public float CameraSensitivity = 150.0f;
    public float CameraRotationY = 15.0f;

    public float CameraPitch = 0.0f;
    public float CameraPitchMin = -5.0f;
    public float CameraPitchMax = 60.0f;

    public float CameraZoomSpeed = 10.0f;

    void Start()
    {
        if (!target)
            target = new GameObject("Target").transform;
    }

    void LateUpdate()
    {
        if (Input.GetButton("Move Camera") && !Input.GetButton("Orbit"))
        {
            target.eulerAngles = new Vector3(target.eulerAngles.x, transform.eulerAngles.y, target.eulerAngles.z);

            target.Translate(new Vector3(-Input.GetAxis("Mouse X"), 0, -Input.GetAxis("Mouse Y")) * moveSpeed * Time.deltaTime);
        }

        //Don't do anything if no target is registered.
        if (target == null)
            return;

        if (Input.GetButton("Orbit") )
        {
            //Move camera up/down
            CameraPitch -= Input.GetAxis("Mouse Y") * CameraSensitivity * Time.deltaTime;
            CameraPitch = Mathf.Clamp(CameraPitch, CameraPitchMin, CameraPitchMax);

            //Move camera left/right
            transform.localEulerAngles = new Vector3(CameraPitch, transform.localEulerAngles.y + Input.GetAxis("Mouse X") * CameraSensitivity * Time.deltaTime, 0);
            transform.localEulerAngles = new Vector3(CameraPitch, transform.localEulerAngles.y, 0);
        }

        //Zoom
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
            CameraDistance -= CameraZoomSpeed * Time.deltaTime;
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
            CameraDistance += CameraZoomSpeed * Time.deltaTime;

        CameraDistance = Mathf.Clamp(CameraDistance, CameraDistanceMin, CameraDistanceMax);

        transform.position = new Vector3(target.position.x, target.position.y, target.position.z);
        transform.position += transform.rotation * Vector3.back * CameraDistance;
        transform.position += Vector3.up * CameraHeight;
    }
}