using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BaseHealth : MonoBehaviour
{
    //Health values
    public int maxHealth = 100;
    public int currentHealth = 100;
    //Stores the last health value
    private int lastHealthValue = 0;

    //The health bar slider
    public Slider healthBar;

    //The text to display health
    public Text healthText;
    //The inital string of that text
    private string initialHealthText;

    void Start()
    {
        //set the initial health text string
        initialHealthText = healthText.text;

        //Update health display
        UpdateHealth();
    }

    void Update()
    {
        //If the health vlue has changed, update health displays
        if (currentHealth != lastHealthValue)
            UpdateHealth();

        //Keeps the current health vlue within constraints
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        if (currentHealth < 0)
            currentHealth = 0;
    }

    //Updates health displays
    void UpdateHealth()
    {
        //Sets the last health vlue
        lastHealthValue = currentHealth;

        //Sets health slider value
        healthBar.value = (float)currentHealth / maxHealth;
        //Set health text
        healthText.text = string.Format(initialHealthText, currentHealth, maxHealth);
    }

    public void Damage(int value)
    {
        currentHealth -= value;
    }

    public void Heal(int value)
    {
        currentHealth += value;
    }
}
