using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public int upgradeCost = 100;

    public int level = 1;
    public int maxLevel = 5;

    public void Upgrade()
    {
        if (level < maxLevel)
        {
            // Check if the player has enough currency to upgrade
            if (MetaCurrency.Instance.CanAfford(upgradeCost))
            {
                MetaCurrency.Instance.SpendMetaCurrency(upgradeCost);
                level++;
                Debug.Log("Upgrade successful! Current level: " + level);
            }
            else
            {
                Debug.Log("Not enough currency to upgrade.");
            }
        }
        else
        {
            Debug.Log("Maximum upgrade level reached.");
        }
    }
}
