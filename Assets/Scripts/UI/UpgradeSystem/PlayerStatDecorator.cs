using UnityEngine;

public abstract class PlayerStatDecorator : IPlayerStats
{
    public IPlayerStats playerStats;

    public PlayerStatDecorator(IPlayerStats playerStats)
    {
        this.playerStats = playerStats;
    }

    public virtual float IncreaseDamage => playerStats.IncreaseDamage;
    public virtual float MaxHealth => playerStats.MaxHealth;
    public virtual float MaxMana => playerStats.MaxMana;
    public virtual float BonusCoin => playerStats.BonusCoin;
}
