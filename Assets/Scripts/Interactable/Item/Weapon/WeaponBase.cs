using System;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour, IWeapon
{
    [Header("Combat")]
    [SerializeField] protected float baseDamage = 10f;
    [SerializeField] protected float cooldown = 0.5f;

    public event Action OnAttack;

    protected float cooldownTimer;
    protected GameObject owner;
    protected float damageMultiplier = 1f;

    protected virtual void Update()
    {
        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;
    }

    public virtual void SetOwner(GameObject owner)
    {
        this.owner = owner;
    }

    public void BoostDamage(float multiplier)
    {
        damageMultiplier *= multiplier;
        OnDamageChanged();
    }

    public void ResetDamage()
    {
        damageMultiplier = 1f;
        OnDamageChanged();
    }

    public float GetDamage()
    {
        return baseDamage * damageMultiplier;
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
}
