using UnityEngine;
using System.Collections;

public class TurretShoot : MonoBehaviour
{
    //The projectile prefab which the turret will fire
    public GameObject projectile;

    //The transform at which the projectile will be instantiated
    public Transform muzzle;

    //Delay between shots
    public float fireTime = 2.0f;
    //Counter till next shot
    private float nextFireTime = 0;

    void Start()
    {
        //Set the initial time till next shot
        nextFireTime = Time.time + fireTime;
    }

    void Update()
    {
        //If the time passed is more than the next fire time...
        if (Time.time > nextFireTime)
        {
            //Set next fire time
            nextFireTime += fireTime;
            //Fire a projectile
            Fire();
        }
    }

    //Fires a projectile
    void Fire()
    {
        //Instantiates a projectile
        GameObject proj = Instantiate(projectile, muzzle.position, muzzle.rotation) as GameObject;
        //Set's the projectiles name
        proj.name = projectile.name;
    }
}
