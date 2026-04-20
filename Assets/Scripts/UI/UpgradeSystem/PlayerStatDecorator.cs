using UnityEngine;

public abstract class PlayerStatDecorator : IPlayerStats
{
    public IPlayerStats playerStats;

    public PlayerStatDecorator(IPlayerStats playerStats)
    {
        this.playerStats = playerStats;
    }

    public virtual float MaxHealth => playerStats.MaxHealth;
}
