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
    public Image gcIcon;
    public Button button;
    public Sprite sprite;
    public string currnetStateTextAddOn = "";

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
        upgradeCurrenStateText.text = $"Current : +{data.upgradeValues[Index].value}{currnetStateTextAddOn}";
        if (Index + 1 >= data.upgradeValues.Length)
        {
            TextMeshProUGUI textMeshProUGUI = button.GetComponentInChildren<TextMeshProUGUI>();
            Image image = button.GetComponent<Image>();
            if (textMeshProUGUI != null)
            {
                textMeshProUGUI.text = "Maxed";
                textMeshProUGUI.color = new Color(1f,1f,1f,1f);
                image.sprite = sprite;
                button.interactable = false;
                upgradeCostText.text = "";
                gcIcon.color = new Color(1f,1f,1f,0f);
                
                return;
            }
        }
        upgradeCostText.text = $"{data.upgradeValues[Index+1].cost}";
    }

    public void OnClick()
    {
        upgradeShop.BuyUpgrade(upgradeData);
    }
}
