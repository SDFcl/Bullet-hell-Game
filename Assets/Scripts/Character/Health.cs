using System;
using Unity.VisualScripting;
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
    public event Action Onhit;

    protected void DeadEvent() => OnDead?.Invoke();
    protected void HealthChangedEvent() => OnHealthChanged?.Invoke(CurrentHP);

    private void Awake()
    {
        CurrentHP = maxHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        if (IsDead || ignoreDamage) return;

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

    public void EnableIgnoreDamage(bool enable)
    {
        ignoreDamage = enable;
    }

}
