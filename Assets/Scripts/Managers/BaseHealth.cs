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
        Destroy(gameObject);
    }
}
