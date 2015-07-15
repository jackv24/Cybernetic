using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TowerTooltip : MonoBehaviour
{
    //The text to display the title of the tower
    public Text titleText;

    //Health bar slider
    public Slider healthBar;
    //Level bar slider
    public Slider levelBar;

    //Where to display health text
    public Text healthText;
    //Initial text value for formatting
    private string initialHealthText;

    //Where to display the level text
    public Text levelText;
    //Initial text value for formatting
    private string initialLevelText;

    //Stores the selected node
    public Node selectedNode;

    //Currently selected tower
    public TowerStats towerStats;

    void Start()
    {
        GameManager.towerTooltip = this;

        //Set initial text values
        initialHealthText = healthText.text;
        initialLevelText = levelText.text;

        gameObject.SetActive(false);
    }

    void Update()
    {
        //If a tower is selected
        if (towerStats)
        {
            //Update name text
            titleText.text = towerStats.towerName;

            //Update slider values
            healthBar.value = (float)towerStats.currentHealth / towerStats.maxHealth;
            levelBar.value = (float)towerStats.currentHealth / towerStats.maxHealth;

            //Update text values
            healthText.text = string.Format(initialHealthText, towerStats.currentHealth, towerStats.maxHealth);
            levelText.text = string.Format(initialLevelText, towerStats.currentLevel, towerStats.maxLevel);
        }
        
    }

    //Clears the selected node
    public void ClearNode()
    {
        //Refunds the tower
        GameManager.resourceManager.Refund(GameManager.selectTowers.selectedTower.GetComponent<TowerStats>());
        //Clears the node
        selectedNode.Clear();
        //Removes tooltip
        GameManager.selectTowers.RemoveTooltip();
    }
}
