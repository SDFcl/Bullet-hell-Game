using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] protected float maxHealth = 100f;
    
    [field: SerializeField]
    public float CurrentHP { get; protected set; }
    public bool IsDead => CurrentHP <= 0;
    private bool ignoreDamage = false;

    public event Action OnDead;
    public event Action<float> OnHealthChanged;

    protected void DeadEvent() => OnDead?.Invoke();
    protected void HealthChangedEvent() => OnHealthChanged?.Invoke(CurrentHP);

    private void Awake()
    {
        CurrentHP = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (IsDead || ignoreDamage) return;

        CurrentHP -= damage;
        HealthChangedEvent();

        Debug.Log($"{gameObject.name} took {damage} damage. Current HP: {CurrentHP}/{maxHealth}");
        if (CurrentHP <= 0)
        {
            Debug.Log($"{gameObject.name} is dead.");
            CurrentHP = 0;
            DeadEvent();
        }
    }

    public void Kill()
    {
        if (IsDead) return;

        CurrentHP = 0;
        HealthChangedEvent();
        DeadEvent();
    }

    public void EnableIgnoreDamage(bool enable)
    {
        ignoreDamage = enable;
    }

}
