using System;
using UnityEngine;

public class PlayerHealth : Health, IHealable
{
    [SerializeField] private float hitIFrameDuration = 0.5f;
    private IFrameController iframe;
    public Action OnIFrame;

    public float MaxHealth
    {
        get => maxHealth;
        set
        {
            //��駤�� MaxHealth ������л�Ѻ CurrentHP �����ҡѺ MaxHealth
            maxHealth = value;
            CurrentHP = maxHealth;
        }
    }
    protected override void Awake()
    {
        base.Awake();
        iframe = GetComponent<IFrameController>();
    }
    public override void TakeDamage(float damage)
    {

        float hpBefore = CurrentHP;

        base.TakeDamage(damage);

        bool tookDamage = CurrentHP < hpBefore;

        if (tookDamage && !IsDead && iframe != null)
        {
            iframe.AddDuration(hitIFrameDuration);
            OnIFrame?.Invoke();
        }
    }
    public void Heal(float amount)
    {
        if (IsDead) return;

        CurrentHP += amount;
        CurrentHP = Mathf.Clamp(CurrentHP, 0, maxHealth);
        HealthChangedEvent();
    }
}
