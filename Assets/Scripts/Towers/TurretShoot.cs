using UnityEngine;
using System.Collections;

//Tower targets script is required
[RequireComponent(typeof(TowerTargets))]
public class TurretShoot : MonoBehaviour
{
    //The projectile prefab which the turret will fire
    public GameObject projectile;

    //The transform at which the projectile will be instantiated
    public Transform muzzle;

    //Delay between shots
    private float fireTime = 2.0f;
    //Counter till next shot
    private float nextFireTime = 0;

    //Reference to towertargets
    private TowerTargets towerTargets;

    void Start()
    {
        //Gets a reference to the tower target script attached to this tower
        towerTargets = GetComponent<TowerTargets>();

        fireTime = GetComponent<TowerStats>().speed;

        //Set the initial time till next shot
        nextFireTime = Time.time + fireTime;
    }

    void Update()
    {
        
        //If the time passed is more than the next fire time...
        if (Time.time > nextFireTime)
        {
            //If there is a target...
            if (towerTargets.target)                
                //Fire a projectile
                Fire();

            //Set next fire time
            nextFireTime += fireTime;
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
