using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] protected float maxHealth = 100f;
    
    [field: SerializeField]
    public float CurrentHP { get; protected set; }
    public bool IsDead => CurrentHP <= 0;

    public event Action OnDead;
    public event Action<float> OnHealthChanged;

    protected void DeadEvent() => OnDead?.Invoke();
    protected void HealthChangedEvent() => OnHealthChanged?.Invoke(CurrentHP);

    private void Start()
    {
        CurrentHP = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (IsDead) return;

        CurrentHP -= damage;
        HealthChangedEvent();

        if (CurrentHP <= 0)
        {
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
}
