using UnityEngine;

public class StatUpgradeDecorator : PlayerStatDecorator
{
    private IPlayerStats baseStats;
    private UpgradeData data;
    private int level;

    public StatUpgradeDecorator(IPlayerStats baseStats, UpgradeData data, int level) : base(baseStats)
    {
        this.baseStats = baseStats;
        this.data = data;
        this.level = level;
    }

    public override float MaxHealth
    {
        get
        {
            if (data.upgradeType == UpgradeType.Health && level > 0)
            {
                float increased = playerStats.MaxHealth + data.upgradeValues[level - 1].value;
                Debug.Log($"[StatUpgradeDecorator] Health: base={playerStats.MaxHealth} + upgrade={data.upgradeValues[level - 1].value} = {increased}");
                return increased;
            }
            return playerStats.MaxHealth;
        }
    }
}
