using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateSlotUpgradeUI : MonoBehaviour
{
    [Header("Upgrade Shop Reference")]
    public UpgradeShop upgradeShop;
    public UpgradeType upgradeType;

    [Header("Upgrade Data")]
    public UpgradeData upgradeData;

    [Header("UI Elements")]
    public TextMeshProUGUI upgradeNameText;
    public Image upgradeIcon;
    public TextMeshProUGUI upgradeDescriptionText;
    public TextMeshProUGUI upgradeCurrenStateText;
    public TextMeshProUGUI upgradeCostText;
    public Button button;

    private void Awake()
    {
        upgradeShop.OnUpgradePurchased += UpdateUI;
    }

    public void UpdateUI(UpgradeData data,int Index)
    {
        if (data.upgradeType != upgradeType)
        {
            return;
        }
        upgradeNameText.text = data.upgradeName;
        upgradeIcon.sprite = data.upgradeIcon;
        upgradeDescriptionText.text = data.upgradeDescription;
        upgradeCurrenStateText.text = $"Current : {data.upgradeValues[Index].value}";
        if (Index + 1 >= data.upgradeValues.Length)
        {
            TextMeshProUGUI textMeshProUGUI = button.GetComponentInChildren<TextMeshProUGUI>();
            if (textMeshProUGUI != null)
            {
                textMeshProUGUI.text = "Maxed";
                button.interactable = false;
                return;
            }
        }
        upgradeCostText.text = $"Cost : {data.upgradeValues[Index+1].cost}";
    }

    public void OnClick()
    {
        upgradeShop.BuyUpgrade(upgradeData);
    }
}
