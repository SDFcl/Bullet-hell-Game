using UnityEngine;
using UnityEngine.UI;


public class UpdateUI : MonoBehaviour
{
    Inventory inventory;

    [Header("UI Elements")]
    [SerializeField] private Image weaponUI; // UI สำหรับอาวุธ
    [SerializeField] private GameObject coinsUI;  // UI สำหรับเหรียญ

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        if (inventory != null)
        {
            inventory.OnWeaponChanged += UpdateWeaponUI;
            inventory.OnCoinsChanged += UpdateCoinsUI;
        }
    }

    private void UpdateWeaponUI(int index)
    {
        if (inventory == null)
        {
            weaponUI.sprite = inventory.Weapons[index].itemData.itemIcon;
        }
    }

    private void UpdateCoinsUI(int coins)
    {
        Debug.Log("UpdateCoinsUI called with coins: " + coins);
    }
}
