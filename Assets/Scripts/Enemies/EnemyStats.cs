using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    //Health values
    public int maxHealth = 100;
    public int currentHealth = 100;

    //Enemy manager to register with
    private EnemyManager enemyManager;

    void Start()
    {
        //Get reference to enemy manager
        enemyManager = GameObject.FindWithTag("GameController").GetComponent<EnemyManager>();
        //Add self to enemies list
        enemyManager.enemies.Add(gameObject);
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
        enemyManager.enemies.Remove(gameObject);

        //Detsroy self
        Destroy(gameObject);
    }
}
