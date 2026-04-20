using UnityEngine;

[System.Serializable]
public class BasePlayerStats : IPlayerStats
{
    public float maxHealth = 100f; // ✅ Default value

    public float MaxHealth => maxHealth;
}
