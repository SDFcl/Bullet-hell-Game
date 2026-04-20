using System;
using UnityEngine;
using System.Collections;

public class PlayerHealth : Health, IHealable
{
    [SerializeField] private float iframeDuration;
    private Coroutine iFrameRoutine;
    public Action<bool> OnIFrame;

    public float MaxHealth
    {
        get => maxHealth;
        set
        {
            //ตั้งค่า MaxHealth ใหม่และปรับ CurrentHP ให้เท่ากับ MaxHealth
            maxHealth = value;
            CurrentHP = maxHealth;
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if (iFrameRoutine != null || ignoreDamage || IsDead) return;
        iFrameRoutine = StartCoroutine(IFrameCount());
    }

    IEnumerator IFrameCount()
    {
        EnableIgnoreDamage(true);
        OnIFrame?.Invoke(true);

        yield return new WaitForSeconds(iframeDuration);
        //Debug.Log("Player can take damage");

        EnableIgnoreDamage(false);
        OnIFrame?.Invoke(false);
        
        iFrameRoutine = null;
    }
    public void Heal(float amount)
    {
        if (IsDead) return;

        CurrentHP += amount;
        CurrentHP = Mathf.Clamp(CurrentHP, 0, maxHealth);
        HealthChangedEvent();
    }
}
