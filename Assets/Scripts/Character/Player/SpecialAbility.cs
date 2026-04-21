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
        }
    }

    public void TryUse()
    {
        if (Attack == null) return;

        // เพิ่มความเสียหายชั่วคราว
        Attack.AddDamagePercent(damageMultiplier - 1); // เพิ่มเป็น damageMultiplier เท่า

        // เริ่มคูลดาวน์
        Invoke(nameof(ResetDamage), duration);
    }

    private void ResetDamage()
    {
        if (Attack == null) return;
        // รีเซ็ตความเสียหายกลับเป็นปกติ
        Attack.AddDamagePercent(1 - damageMultiplier); // ลดกลับเป็น 1 เท่า
    }
}
