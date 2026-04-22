using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgradeManager : MonoBehaviour
{
    [SerializeField] private BasePlayerStats playerStats;
    private Dictionary<string, int> currentUpgradeLevels = new();
    private Dictionary<string, UpgradeData> upgradeLookup = new(); // 🔥 NEW
    private IPlayerStats finalStats;

    public void RegisterUpgrade(UpgradeData data) // 🔥 NEW - เรียกจาก UpgradeShop
    {
        upgradeLookup[data.Id] = data;
    }

    public int GetLevel(UpgradeData data)
    {
        return currentUpgradeLevels.ContainsKey(data.Id) ? currentUpgradeLevels[data.Id] : 0;
    }

    public bool CanUpgrade(UpgradeData data, int currency)
    {
        int level = GetLevel(data);
        if (level >= data.upgradeValues.Length) return false;

        return currency >= data.upgradeValues[level].cost;
    }

    public void ApplyUpgrade(UpgradeData data)
    {
        string id = data.Id;
        if (!currentUpgradeLevels.ContainsKey(id))
            currentUpgradeLevels[id] = 0;
        currentUpgradeLevels[id]++;
        Rebuild();
    }

    private void Rebuild()
    {
        IPlayerStats baseStats = playerStats;
        foreach (var kv in currentUpgradeLevels)
        {
            if (upgradeLookup.ContainsKey(kv.Key)) // 🔥 ค้นหา UpgradeData จาก ID
            {
                baseStats = new StatUpgradeDecorator(baseStats, upgradeLookup[kv.Key], kv.Value);
            }
        }
        finalStats = baseStats;
    }

    public IPlayerStats GetFinalStats()
    {
        Rebuild(); // 🔥 Ensure stats are up-to-date before returning
        return finalStats;
    }


}