using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TowerTargets))]
public class TurretLook : MonoBehaviour
{
    //The target which the turret will look at
    private Transform target;

    //The transform for the mechanical turret parts
    public Transform rotateTransform;
    public Transform pivotTransform;

    //The spped at which the turret will rotate
    public float rotateSpeed = 0.25f;

    private TowerTargets towerTargets;

    void Start()
    {
        towerTargets = GetComponent<TowerTargets>();
    }

    void Update()
    {
        target = towerTargets.target;

        //If there is a target, rotate towards it
        if (target)
            RotateTowards(target.position);
    }

    //Rotates the turret towards a target position
    void RotateTowards(Vector3 position)
    {
        //If the rotate transform exists...
        if (rotateTransform)
        {
            //Get distance to target from rotate transform
            Vector3 direction = position - rotateTransform.position;

            //Rotate towards direction
            rotateTransform.rotation = Quaternion.Lerp(rotateTransform.rotation, Quaternion.LookRotation(direction), rotateSpeed * Time.timeScale);

            //Constrain to y axis
            rotateTransform.eulerAngles = new Vector3(0, rotateTransform.eulerAngles.y, 0);
        }

        //If the pivot transform exists...
        if (pivotTransform)
        {
            //Get distance to target from pivot transform
            Vector3 direction = position - pivotTransform.position;

            //Rotate towards direction
            pivotTransform.rotation = Quaternion.Lerp(pivotTransform.rotation, Quaternion.LookRotation(direction), rotateSpeed);

            //Constrain to x axis, following rotate y axis
            pivotTransform.eulerAngles = new Vector3(pivotTransform.eulerAngles.x, rotateTransform.eulerAngles.y, 0);
        }
    }
}
