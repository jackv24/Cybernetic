using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TowerTooltip : MonoBehaviour
{
    public Text titleText;

    public Slider healthBar;
    public Slider levelBar;

    public Text healthText;
    private string initialHealthText;

    public Text levelText;
    private string initialLevelText;

    //Stores the selected node
    public Node selectedNode;

    public TowerStats towerStats;

    [HideInInspector] //Stores a reference to the selectTowers script (which is set by that script, hence the public variable)
    public SelectTowers selectTowers;

    //Stores a reference to the resource manager
    private ResourceManager resourceManager;

    void Start()
    {
        //gets a reference to the resourcemanager script attached to the game controller
        resourceManager = GameObject.FindWithTag("GameController").GetComponent<ResourceManager>();

        initialHealthText = healthText.text;
        initialLevelText = levelText.text;

        gameObject.SetActive(false);
    }

    void Update()
    {
        if (towerStats)
        {
            titleText.text = towerStats.towerName;

            healthBar.value = (float)towerStats.currentHealth / towerStats.maxHealth;
            levelBar.value = (float)towerStats.currentHealth / towerStats.maxHealth;

            healthText.text = string.Format(initialHealthText, towerStats.currentHealth, towerStats.maxHealth);
            levelText.text = string.Format(initialLevelText, towerStats.currentLevel, towerStats.maxLevel);
        }
    }

    //Clears the selected node
    public void ClearNode()
    {
        //Refunds the tower
        resourceManager.Refund(selectTowers.selectedTower.GetComponent<TowerStats>());
        //Clears the node
        selectedNode.Clear();
        //Removes tooltip
        selectTowers.RemoveTooltip();
    }
}
