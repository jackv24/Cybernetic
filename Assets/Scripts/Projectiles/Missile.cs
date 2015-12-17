using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour
{
    public string targetTag = "Enemy";

    //The speed at which the laser will travel
    public float speed = 1f;

    public float acceleration = 0.25f;

    //How long will it last?
    public float lifeTime = 3f;
    //How long has it lasted?
    private float life = 0;

    public int damage = 50;

    public GameObject explosionPrefab;

    void Update()
    {
        speed += acceleration * Time.deltaTime;

        //Increase the time it has been "alive"
        life += Time.deltaTime;

        //If the laser is still within its lifetime
        if (life < lifeTime)
        {
            //Move forward with respect to speed and time
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
            Explode();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == targetTag)
        {
            EnemyStats health = col.gameObject.GetComponent<EnemyStats>();

            health.RemoveHealth(damage);

            life = lifeTime;
        }
        else if(col.gameObject.tag == "Path")
        {
            life = lifeTime;
        }
    }

    void Explode()
    {
        GameObject obj = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;

        Destroy(obj, 3f);

        Destroy(gameObject);
    }
}
