using UnityEngine;

public class PlayerHealth : Health, IHealable
{
    public void Heal(float amount)
    {
        if (IsDead) return;

        CurrentHP += amount;
        CurrentHP = Mathf.Clamp(CurrentHP, 0, maxHealth);
        HealthChangedEvent();
    }
}
