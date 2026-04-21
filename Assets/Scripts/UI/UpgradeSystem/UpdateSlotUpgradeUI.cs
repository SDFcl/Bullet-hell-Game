using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateSlotUpgradeUI : MonoBehaviour
{
    [Header("Upgrade Shop Reference")]
    public UpgradeShop upgradeShop;
    public UpgradeType upgradeType;

    [Header("UI Elements")]
    public TextMeshProUGUI upgradeNameText;
    public Image upgradeIcon;
    public TextMeshProUGUI upgradeDescriptionText;
    public TextMeshProUGUI upgradeCurrenStateText;
    public TextMeshProUGUI upgradeCostText;

    private void Awake()
    {
        upgradeShop.OnUpgradePurchased += UpdateUI;
    }

    public void UpdateUI(UpgradeData data,int Index)
    {
        Debug.Log($"[UpdateSlotUpgradeUI] Received upgrade purchase event: {data.upgradeName} level {Index}. Upgrade type: {data.upgradeType}, Slot upgrade type: {upgradeType}");
        if (data.upgradeType != upgradeType)
        {
            return;
        }
        upgradeNameText.text = data.upgradeName;
        upgradeIcon.sprite = data.upgradeIcon;
        upgradeDescriptionText.text = data.upgradeDescription;
        upgradeCurrenStateText.text = $"Current : {data.upgradeValues[Index].value}";
        upgradeCostText.text = $"Cost : {data.upgradeValues[Index].cost}";
    }
}
