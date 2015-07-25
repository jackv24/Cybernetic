using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    //Health values
    public int maxHealth = 100;
    public int currentHealth = 100;

    public int resources = 2;

    void Start()
    {
        //Add self to enemies list
        GameManager.enemyManager.currentEnemies.Add(gameObject);
    }

    void Update()
    {
        //If health drops below zero, die
        if (currentHealth <= 0)
            Die();

        //Keeps the current health vlue within constraints
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        if (currentHealth < 0)
            currentHealth = 0;
    }

    public void RemoveHealth(int value)
    {
        currentHealth -= value;
    }

    public void AddHealth(int value)
    {
        currentHealth += value;
    }

    void Die()
    {
        //Remove self from enemies list
        GameManager.enemyManager.RegisterDeath(gameObject, resources);

        //Detsroy self
        Destroy(gameObject);
    }
}
