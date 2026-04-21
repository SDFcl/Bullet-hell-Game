using System;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;

public class UpgradeShop : MonoBehaviour
{
    [Header("References")]
    public PlayerUpgradeManager manager;

    [Header("UI")]
    public Toggle toggle;
    public CanvasGroup canvasGroup;

    public event Action<UpgradeData,int> OnUpgradePurchased;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (manager == null)
        {
            manager = FindObjectOfType<PlayerUpgradeManager>();
            if (manager == null)
            {
                Debug.LogError("UpgradeShop: PlayerUpgradeManager not found in the scene.");
            }
        }

    }

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
        if (!MetaCurrency.Instance.CanAfford(cost))
        {
            Debug.Log("เงินไม่พอ");
            return;
        }

        MetaCurrency.Instance.SpendMetaCurrency(cost);
        manager.ApplyUpgrade(data);
        OnUpgradePurchased?.Invoke(data,nextLevel);

        Debug.Log($"[UpgradeShop] Bought {data.upgradeName} level {nextLevel}. {data.upgradeValues[nextLevel].value}");
    }
    public void ChangeState()
    {
        canvasGroup.alpha = toggle.isOn ? 1f : 0f;
        canvasGroup.interactable = toggle.isOn;
        canvasGroup.blocksRaycasts = toggle.isOn;
    }
}
