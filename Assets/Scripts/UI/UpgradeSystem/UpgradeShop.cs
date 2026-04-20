using System;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeShop : MonoBehaviour
{
    public PlayerUpgradeManager manager;
    public int currency;

    public void BuyUpgrade(UpgradeData data)
    {
        int currentLevel = manager.GetLevel(data);
        int nextLevel = currentLevel + 1;

        // 🔒 กัน index หลุด
        if (nextLevel >= data.upgradeValues.Length)
        {
            Debug.Log("อัพเกรดเต็มแล้ว");
            return;
        }

        int cost = data.upgradeValues[nextLevel].cost;

        // 🔒 เช็คเงินทีหลัง (ปลอดภัยกว่า)
        if (currency < cost)
        {
            Debug.Log("เงินไม่พอ");
            return;
        }

        currency -= cost;
        manager.ApplyUpgrade(data);
        
        // ✅ Update stats เลย!
        UpdatePlayerStats();
        
        Debug.Log($"[UpgradeShop] Bought {data.upgradeName} level {nextLevel}. Remaining currency: {currency}");
    }

    private void UpdatePlayerStats()
    {
        var playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            var stats = manager.GetFinalStats();
            var health = playerController.GetComponent<PlayerHealth>();
            
            if (health != null && stats != null)
            {
                health.MaxHealth = stats.MaxHealth;
                Debug.Log($"[UpgradeShop] Updated MaxHealth to {stats.MaxHealth}");
            }
        }
    }
}
