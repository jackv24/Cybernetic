using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{
    //The speed at which the laser will travel
    public float speed = 5f;

    //How long will it last?
    public float lifeTime = 2f;

    //How long has it lasted?
    private float life = 0;

    void Update()
    {
        //Increase the time it has been "alive"
        life += Time.deltaTime;

        //If the laser is still within its lifetime
        if(life < lifeTime)
            //Move forward with respect to speed and time
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
