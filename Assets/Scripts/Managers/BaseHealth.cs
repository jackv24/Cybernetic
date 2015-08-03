using UnityEngine;
using System.Collections;

public class BaseHealth : MonoBehaviour
{
    //Health values
    public int maxHealth = 100;
    public int currentHealth = 100;

    void Start()
    {
        GameManager.baseHealth = this;
    }

    void Update()
    {
        if (currentHealth <= 0)
            Die();

        //Keeps the current health vlue within constraints
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        if (currentHealth < 0)
            currentHealth = 0;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();

            RemoveHealth(enemy.attackPower);

            enemy.Die();
        }
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
        GameManager.gameManager.EndGame(false);
    }
}
