using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Weapons")]
    public int maxWeapons = 5;
    public List<InventoryItem> Weapons = new List<InventoryItem>();

    [Header("Consumables")]
    public int maxConsumables = 1;
    public List<InventoryItem> Consumables = new List<InventoryItem>();

    [Header("Currency")]
    [SerializeField]
    public int Coins = 0;
    public int CurrentCoin => Coins;

    #region Events
    public Action<int> OnWeaponChanged;
    public Action OnConsumableChanged;
    public Action<int> OnCoinsChanged;
    #endregion

    #region Weapon
    void Start()
    {
        Invoke(nameof(DealySetup),0.01f);
    }
    void DealySetup()
    {
        OnConsumableChanged?.Invoke();
    }
    public bool AddWeapon(InventoryItem item)
    {
        if (Weapons.Count == 0)
        {
            Weapons.Add(item);
            OnWeaponChanged?.Invoke(0);
            // Debug.Log("Added weapon: " + item.itemData.itemName + " at index 0");
            SaveInventory();
            return true;
        }

        if (Weapons.Count >= maxWeapons)
        {
            HoldingWeapon holdingWeapon = FindObjectOfType<HoldingWeapon>();
            int currentIndex = holdingWeapon.currentIndex;
            //if (currentIndex == 0)
            //{
            //    Debug.Log("Cannot add weapon: " + item.itemData.itemName + " because current holding weapon index is 0 and max weapons reached.");
            //    return false;
            //}
            DropWeapon(currentIndex);
            Weapons.Insert(currentIndex, item);

            OnWeaponChanged?.Invoke(currentIndex);
            SaveInventory();
            return true;
        }

        Weapons.Add(item);
        int newIndex = Weapons.Count - 1;
        OnWeaponChanged?.Invoke(newIndex);
        SaveInventory();
        return true;
    }

    public void DropWeapon(int index)
    {
        if (index < 0 || index >= Weapons.Count) return;

        InventoryItem item = Weapons[index];

        Instantiate(item.itemData.WorldPrefab, this.transform.position, Quaternion.identity);

        Weapons.RemoveAt(index);
    }
    #endregion
    #region Consumable
    public void AddConsumable(InventoryItem item)
    {
        if (Consumables.Count >= maxConsumables)
        {
            DropConsumable(0);
            Consumables.Insert(0, item);
            OnConsumableChanged?.Invoke();
            SaveInventory();
            return;
        }
        Consumables.Add(item);
        OnConsumableChanged?.Invoke();
        SaveInventory();

    }

    public void DropConsumable(int index)
    {
        if (index < 0 || index >= Consumables.Count) return;
        InventoryItem item = Consumables[index];
        if (item.itemData.consumableType == ConsumableType.passive)
        {
            foreach (var effect in item.itemData.effects)
            {
                effect.Apply(this.gameObject, true);
            }
        }
        
        Instantiate(item.itemData.WorldPrefab, this.transform.position, Quaternion.identity);
        Consumables.RemoveAt(index);
    }
    #endregion
    #region Currency
    public void AddCoins(int amount)
    {
        Coins += amount;
        OnCoinsChanged?.Invoke(Coins);
    }
    public bool SpendCoins(int amount)
    {
        if (Coins < amount) return false;
        Coins -= amount;
        OnCoinsChanged.Invoke(Coins);
        return true;
    }
    #endregion
    #region Save/Load
    public void SaveInventory()
    {
        GameSession.savedInventory.weapons = new List<InventoryItem>();

        foreach (var item in Weapons)
        {
            GameSession.savedInventory.weapons.Add(item);
        }

        GameSession.savedInventory.consumables = new List<InventoryItem>();

        foreach (var item in Consumables)
        {
            GameSession.savedInventory.consumables.Add(item);
        }

        GameSession.savedInventory.coins = Coins;
    }

    public void LoadInventory()
    {
        Weapons.Clear();

        foreach (var item in GameSession.savedInventory.weapons)
        {
            Weapons.Add(item);
        }

        Consumables.Clear();

        foreach (var item in GameSession.savedInventory.consumables)
        {
            Consumables.Add(item);
        }

        Coins = GameSession.savedInventory.coins;
        OnCoinsChanged?.Invoke(Coins);

        if(Weapons.Count > 0)
        {
            OnWeaponChanged?.Invoke(0);
        }
    }
    #endregion
}