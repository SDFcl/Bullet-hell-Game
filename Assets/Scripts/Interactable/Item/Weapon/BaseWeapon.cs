using System;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour, IWeapon
{
    [Header("Combat")]
    [SerializeField] protected float baseDamage = 10f;
    [SerializeField] protected float currentDamage;
    [SerializeField] protected float cooldown = 0.5f;

    public event Action OnAttack;

    protected float cooldownTimer;
    protected GameObject owner;
    protected float damagePercent = 0f;
    protected float flatDamage = 0f;

    protected virtual void Update()
    {
        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;

        // Debug for testing damage adjustments
        currentDamage = GetDamage();
    }

    public virtual void SetOwner(GameObject owner)
    {
        this.owner = owner;
    }

    public float GetDamage()
    {
        return (baseDamage + flatDamage) * (1f + damagePercent);
    }

    public void ExecuteAttack()
    {
        if (cooldownTimer > 0f)
            return;

        if (!CanAttack())
            return;

        PerformAttack();
        cooldownTimer = cooldown;
        OnAttack?.Invoke();
    }

    protected virtual bool CanAttack() => true;

    protected virtual void OnDamageChanged() { }

    protected abstract void PerformAttack();

    #region AdjustDamage API
    public void AddDamagePercent(float percent)
    {
        damagePercent += percent;
        OnDamageChanged();
    }

    public void RemoveDamagePercent(float percent)
    {
        damagePercent -= percent;
        if (damagePercent < 0f) damagePercent = 0f;
        OnDamageChanged();
    }

    public void ResetDamagePercent()
    {
        damagePercent = 0f;
        OnDamageChanged();
    }
    public void AddFlatDamage(float amount)
    {
        flatDamage += amount;
        OnDamageChanged();
    }
    public void RemoveFlatDamage(float amount)
    {
        flatDamage -= amount;
        if (flatDamage < 0f) flatDamage = 0f;
        OnDamageChanged();
    }
    public void ResetFlatDamage()
    {   
        flatDamage = 0f;
        OnDamageChanged();
    }
    #endregion
}
