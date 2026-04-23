using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] protected float maxHealth = 100f;
    
    [field: SerializeField]
    public float CurrentHP { get; protected set; }
    public bool IsDead => CurrentHP <= 0;
    private IDamageBlocker[] damageBlockers;

    public event Action OnDead;
    public event Action<float> OnHealthChanged;
    public event Action Onhit;

    protected void DeadEvent() => OnDead?.Invoke();
    protected void HealthChangedEvent() => OnHealthChanged?.Invoke(CurrentHP);

    protected virtual void Awake()
    {
        if (GameSession.savedHealth > 0)
        {
            CurrentHP = GameSession.savedHealth;
        }
        else
        {
            CurrentHP = maxHealth;
        }
        damageBlockers = GetComponents<IDamageBlocker>();
    }

    public virtual void TakeDamage(float damage)
    {
        if (IsDead || IgnoreDamage) return;

        foreach (IDamageBlocker blocker in damageBlockers)
        {
            if (blocker.IsDamageBlocked)
                return;
        }

        CurrentHP -= damage;
        HealthChangedEvent();
        Onhit?.Invoke();

        Debug.Log($"{gameObject.name} took {damage} damage. Current HP: {CurrentHP}/{maxHealth}");
        if (CurrentHP <= 0)
        {
            Debug.Log($"{gameObject.name} is dead.");
            CurrentHP = 0;
            GetComponent<Collider2D>().enabled = false;
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

    public bool IgnoreDamage { get; private set; } = false;
    public void EnableIgnoreDamage(bool enable)
    {
        IgnoreDamage = enable;
    }

    public void SaveHealth()
    {
        GameSession.savedHealth = (int)CurrentHP;
    }   
}
