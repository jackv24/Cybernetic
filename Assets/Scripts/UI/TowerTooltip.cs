using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TowerTooltip : MonoBehaviour
{
    //The text to display the title of the tower
    public Text titleText;

    //Image to display icon
    public Image towerIcon;

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

    public Text infoText;
    private string infoTextString;

    public Button upgradeButton;
    public Text upgradeText;

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
        infoTextString = infoText.text;

        gameObject.SetActive(false);
    }

    void Update()
    {
        //If a tower is selected
        if (towerStats)
        {
            //Update name text
            titleText.text = towerStats.towerName;

            //sets tower icon
            if (towerStats.icon)
                towerIcon.sprite = towerStats.icon;

            //Update slider values
            healthBar.value = (float)towerStats.currentHealth / towerStats.levels[towerStats.currentLevel].maxHealth;
            levelBar.value = (float)(towerStats.currentLevel+1) / towerStats.levels.Length;

            //Update text values
            healthText.text = string.Format(initialHealthText, towerStats.currentHealth, towerStats.levels[towerStats.currentLevel].maxHealth);
            levelText.text = string.Format(initialLevelText, towerStats.currentLevel + 1, towerStats.levels.Length);

            if (towerStats.currentLevel + 1 < towerStats.levels.Length)
            {
                infoText.text = string.Format(infoTextString,
                    towerStats.levels[towerStats.currentLevel + 1].maxHealth,
                    towerStats.levels[towerStats.currentLevel + 1].speed,
                    towerStats.levels[towerStats.currentLevel + 1].range
                    );

                upgradeText.text = string.Format("Upgrade ({0})", towerStats.levels[towerStats.currentLevel + 1].cost);

                if (GameManager.resourceManager.resources < towerStats.levels[towerStats.currentLevel + 1].cost)
                    upgradeButton.interactable = false;
                else
                    upgradeButton.interactable = true;
            }
            else
            {
                infoText.text = "Fully Upgraded";

                upgradeButton.interactable = false;
                upgradeText.text = "Upgrade (X)";
            }
        }
        
    }

    public void SetSelected(TowerStats tower)
    {
        towerStats = tower;
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

    public void Upgrade()
    {
        towerStats.Upgrade();
    }
}
