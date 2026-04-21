using UnityEngine;

[System.Serializable]
public class BasePlayerStats : IPlayerStats
{
    public float increaseDamage = 0f; // ✅ Default value
    public float maxHealth = 100f; // ✅ Default value
    public float maxMana = 50f; // ✅ Default value
    public float bonusCoin = 0f; // ✅ Default value

    public float IncreaseDamage => increaseDamage;
    public float MaxHealth => maxHealth;
    public float MaxMana => maxMana;
    public float BonusCoin => bonusCoin;
}
