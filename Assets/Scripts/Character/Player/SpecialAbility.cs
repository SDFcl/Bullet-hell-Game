using UnityEngine;

public class SpecialAbility : MonoBehaviour
{
    public float damageMultiplier = 1.5f; // ตัวคูณความเสียหายของสกิลพิเศษ
    public float duration = 3f; // ระยะเวลาของสกิลพิเศษ
    public float cooldownTime = 5f; // เวลาคูลดาวน์ของสกิลพิเศษ

    private Attack Attack;
    private PlayerUpgradeManager upgradeManager;

    private void Awake()
    {
        Attack = GetComponent<Attack>();
        upgradeManager = FindObjectOfType<PlayerUpgradeManager>();
        if(upgradeManager != null)
        {
            IPlayerStats stats = upgradeManager.GetFinalStats();
            damageMultiplier += stats.IncreaseDamage/100; // เพิ่มความเสียหายจากอัพเกรด
            Debug.Log($"[SpecialAbility] Damage multiplier after applying upgrades: {damageMultiplier}. {stats.IncreaseDamage}");
        }
    }

    public void TryUse()
    {
        if (Attack == null) return;

        // เพิ่มความเสียหายชั่วคราว
        Attack.AddDamagePercent(damageMultiplier); // เพิ่มเป็น damageMultiplier เท่า

        //Animation หรือ VFX สำหรับสกิลพิเศษสามารถใส่ตรงนี้ได้


        // เริ่มคูลดาวน์
        Invoke(nameof(ResetDamage), duration);
    }

    private void ResetDamage()
    {
        if (Attack == null) return;
        // รีเซ็ตความเสียหายกลับเป็นปกติ
        Attack.RemoveDamagePercent(damageMultiplier); // ลดกลับเป็น 1 เท่า
        //Animation หรือ VFX สำหรับรีเซ็ตสกิลพิเศษสามารถใส่ตรงนี้ได้


    }
}
