using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BaseHealthDisplay : MonoBehaviour
{
    public Slider healthBar;
    public Text healthText;

    private BaseHealth baseHealth;

    void Start()
    {
        baseHealth = GameManager.baseHealth;
    }

    void Update()
    {
        healthBar.value = (float)baseHealth.currentHealth / baseHealth.maxHealth;

        healthText.text = "Health: " + baseHealth.currentHealth + "/" + baseHealth.maxHealth;
    }
}
