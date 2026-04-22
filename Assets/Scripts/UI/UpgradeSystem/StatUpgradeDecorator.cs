using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatUpgradeDecorator : PlayerStatDecorator
{
    private IPlayerStats baseStats;
    private UpgradeData data;
    private Dictionary<UpgradeType, int> upgradeLevels = new Dictionary<UpgradeType, int>();

    public StatUpgradeDecorator(IPlayerStats baseStats, UpgradeData data, int level) : base(baseStats)
    {
        this.baseStats = baseStats;
        this.data = data;
        upgradeLevels[data.upgradeType] = level;
    }

    public override float IncreaseDamage
    {
        get
        {
            if (data.upgradeType == UpgradeType.IncrementalDamage)
            {
                int level = upgradeLevels.ContainsKey(UpgradeType.IncrementalDamage) ? upgradeLevels[UpgradeType.IncrementalDamage] : 0;
                if (level > 0 && level <= data.upgradeValues.Length)
                {
                    int index = level;
                    float upgradeValue = data.upgradeValues[index].value;
                    float increased = playerStats.IncreaseDamage + upgradeValue;
                    Debug.Log($"IncreaseDamage: base={playerStats.IncreaseDamage} + Lv{level}={upgradeValue} = {increased}");
                    return increased;
                }
            }
            return playerStats.IncreaseDamage;
        }
    }

    public override float MaxHealth
    {
        get
        {
            if (data.upgradeType == UpgradeType.Health)
            {
                int level = upgradeLevels.ContainsKey(UpgradeType.Health) ? upgradeLevels[UpgradeType.Health] : 0;
                if (level > 0 && level <= data.upgradeValues.Length)
                {
                    int index = level;
                    float upgradeValue = data.upgradeValues[index].value;
                    float increased = playerStats.MaxHealth + upgradeValue;

                    Debug.Log($"Health: base={playerStats.MaxHealth} + Lv{level}={upgradeValue} = {increased}");
                    return increased;
                }
            }
            return playerStats.MaxHealth;
        }
    }

    public override float MaxMana
    {
        get
        {
            if (data.upgradeType == UpgradeType.Mana)
            {
                int level = upgradeLevels.ContainsKey(UpgradeType.Mana) ? upgradeLevels[UpgradeType.Mana] : 0;
                if (level > 0 && level <= data.upgradeValues.Length)
                {
                    int index = level;
                    float upgradeValue = data.upgradeValues[index].value;
                    float increased = playerStats.MaxMana + upgradeValue;
                    Debug.Log($"Mana: base={playerStats.MaxMana} + Lv{level}={upgradeValue} = {increased}");
                    return increased;
                }
            }
            return playerStats.MaxMana;
        }
    }

    public override float BonusCoin
    {
        get
        {
            if (data.upgradeType == UpgradeType.BonusCoin)
            {
                int level = upgradeLevels.ContainsKey(UpgradeType.BonusCoin) ? upgradeLevels[UpgradeType.BonusCoin] : 0;
                if (level > 0 && level <= data.upgradeValues.Length)
                {
                    int index = level;
                    float upgradeValue = data.upgradeValues[index].value;
                    float increased = playerStats.BonusCoin + upgradeValue;

                    Debug.Log($"BonusCoin: base={playerStats.BonusCoin} + Lv{level}={upgradeValue} = {increased}");
                    return increased;
                }
            }
            return playerStats.BonusCoin;
        }
    }
}
