using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerUpgradeManager : MonoBehaviour
{
    public BasePlayerStats playerStats;

    private Dictionary<UpgradeData, int> currentUpgradeLevels = new();
    private IPlayerStats finalStats;

    public IPlayerStats GetFinalStats()
    {
        Rebuild();
        return finalStats;
    }

    public bool CanUpgrade(UpgradeData data, int currentCurrency)
    {
        int level = GetLevel(data) + 1;
        if (level > data.upgradeValues.Length) return false;

        return currentCurrency >= data.upgradeValues[level-1].cost;
    }

    public void ApplyUpgrade(UpgradeData data)
    {
        if(!currentUpgradeLevels.ContainsKey(data))
            currentUpgradeLevels[data] = 0;

        currentUpgradeLevels[data]++;
    }

    public int GetLevel(UpgradeData data)
    {
        return currentUpgradeLevels.ContainsKey(data) ? currentUpgradeLevels[data] : 0;
    }

    // The error CS0266 occurs because 'StatUpgradeDecorator' does not implement the 'IPlayerStats' interface directly.  
    // To fix this, you need to ensure that 'StatUpgradeDecorator' either implements 'IPlayerStats' or explicitly cast it to 'IPlayerStats'.  

    private void Rebuild()
    {
        IPlayerStats baseStats = playerStats;

        foreach (var up in currentUpgradeLevels)
        {
            baseStats = (IPlayerStats)new StatUpgradeDecorator(baseStats, up.Key, up.Value);
        }

        finalStats = baseStats;
    }
}
