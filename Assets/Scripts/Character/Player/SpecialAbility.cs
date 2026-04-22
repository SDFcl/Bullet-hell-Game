using System;
using NUnit.Framework;
using UnityEngine;

public class SpecialAbility : MonoBehaviour
{
    public float damageMultiplier = 1.5f; // ��Ǥٳ����������¢ͧʡ�ž����
    public float duration = 3f; // �������Ңͧʡ�ž����
    public float cooldownTime = 5f; // ���Ҥ�Ŵ�ǹ�ͧʡ�ž����
    float cooldowntimer;
    public float CurrentCooldown => Mathf.Clamp01(cooldowntimer / cooldownTime);


    private Attack Attack;
    private PlayerUpgradeManager upgradeManager;

    public Action<float> OnActive;

    private void Awake()
    {
        Attack = GetComponent<Attack>();
        PlayerUpgradeManager playerUpgradeManager = FindObjectOfType<PlayerUpgradeManager>();
        if(playerUpgradeManager != null)
        {
            IPlayerStats stats = playerUpgradeManager.GetFinalStats();
            damageMultiplier += stats.IncreaseDamage/100; // ��������������¨ҡ�Ѿ�ô
            Debug.Log($"[SpecialAbility] Damage multiplier after applying upgrades: {damageMultiplier}. {stats.IncreaseDamage}");
        }
    }
    void Start()
    {
        cooldowntimer = cooldownTime;
    }
    void Update()
    {
        cooldowntimer += Time.deltaTime;
    }

    public void TryUse()
    {
        if (Attack == null) return;

        if(cooldowntimer >= cooldownTime)
        {
            cooldowntimer = 0;
            Attack.AddDamagePercent(damageMultiplier);
            Invoke(nameof(ResetDamage), duration);

            OnActive?.Invoke(duration);
            //Debug.Log("Add Sucess");
        }
        else
        {
            //Debug.Log("Is cooldown");
        }
    }

    private void ResetDamage()
    {
        if (Attack == null) return;
        // ���絤���������¡�Ѻ�繻���
        Attack.RemoveDamagePercent(damageMultiplier); // Ŵ��Ѻ�� 1 ���
        //Animation ���� VFX ����Ѻ����ʡ�ž��������ö���ç�����


    }
}
