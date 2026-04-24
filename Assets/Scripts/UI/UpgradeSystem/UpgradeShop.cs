using System;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class UpgradeShop : MonoBehaviour, IDataPersistence
{
    [Header("References")]
    public PlayerUpgradeManager manager;

    [Header("Upgrade Data")]
    [SerializeField] private List<UpgradeData> allUpgrades;
    private Dictionary<string, UpgradeData> lookup = new();

    [Header("UI")]
    public Toggle toggle;
    public CanvasGroup canvasGroup;

    public event Action<UpgradeData,int> OnUpgradePurchased;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (manager == null)
        {
            manager = FindObjectOfType<PlayerUpgradeManager>();
        }
        
        // ?? Register �ء upgrade
        foreach (var up in allUpgrades)
        {
            lookup[up.Id] = up;
            manager.RegisterUpgrade(up);  // ?? NEW
            OnUpgradePurchased?.Invoke(up, manager.GetLevel(up)); // ?? NEW - �Ѿവ UI �͹�������
        }

    }

    public void BuyUpgrade(UpgradeData data)
    {
        int currentLevel = manager.GetLevel(data);
        int nextLevel = currentLevel + 1;

        // ?? �ѹ index ��ش
        if (nextLevel >= data.upgradeValues.Length)
        {
            Debug.Log("�Ѿ�ô�������");
            return;
        }

        int cost = data.upgradeValues[nextLevel].cost;

        // ?? ���Թ����ѧ (��ʹ��¡���)
        if (!MetaCurrency.Instance.CanAfford(cost))
        {
            Debug.Log("�Թ����");
            return;
        }

        MetaCurrency.Instance.SpendMetaCurrency(cost);
        manager.ApplyUpgrade(data);
        OnUpgradePurchased?.Invoke(data,nextLevel);

        Debug.Log($"[UpgradeShop] Bought {data.upgradeName} level {nextLevel}. {data.upgradeValues[nextLevel].value}");
    }

    // ======================
    // SAVE / LOAD
    // ======================
    public void LoadData(GameData data)
    {
        //Debug.Log($"[UpgradeShop] Loading upgrades...");
        foreach (var up in allUpgrades)
        {
            //Debug.Log($"[UpgradeShop] Loading {up.upgradeName}...");
            int savedLevel = data.upgradeLevels.ContainsKey(up.Id) ? data.upgradeLevels[up.Id] : 0;
            // Debug.Log($"[UpgradeShop] Saved level for {up.upgradeName}: {savedLevel}");
            for (int i = 0; i < savedLevel; i++)
            {
                manager.ApplyUpgrade(up);
                OnUpgradePurchased?.Invoke(up, i + 1);
                Debug.Log($"[UpgradeShop] Loaded {up.upgradeName} level {i + 1}. {up.upgradeValues[i + 1].value}");
            }
        }
    }

    public void SaveData(ref GameData data)
    {
        foreach (var up in allUpgrades)
        {
            if (data.upgradeLevels.ContainsKey(up.Id))
            {
                data.upgradeLevels.Remove(up.Id);
            }
            Debug.Log($"[UpgradeShop] Saving {up.upgradeName}...");
            int currentLevel = manager.GetLevel(up);
            data.upgradeLevels.Add(up.Id, currentLevel);
            Debug.Log($"{data.upgradeLevels.ContainsKey(up.Id)}");
        }
    }

    public void ChangeState()
    {
        canvasGroup.alpha = toggle.isOn ? 1f : 0f;
        canvasGroup.interactable = toggle.isOn;
        canvasGroup.blocksRaycasts = toggle.isOn;
    }
}



