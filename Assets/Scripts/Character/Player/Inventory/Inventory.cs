using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Weapons")]
    public int maxWeapons = 5;
    public List<Item> Weapons = new List<Item>();

    [Header("Consumables")]
    public int maxConsumables = 1;
    public List<Item> Consumables = new List<Item>();

    [Header("Currency")]
    [SerializeField]
    private int Coins = 0;

    #region Events
    public Action<int> OnWeaponChanged;
    public Action<int> OnCoinsChanged;
    #endregion

    #region Weapon
    public void AddWeapon(Item item)
    {
        if (Weapons.Count == 0)
        {
            Weapons.Add(item);
            OnWeaponChanged?.Invoke(0);
            Debug.Log("Added weapon: " + item.itemData.itemName + " at index 0");
            return;
        }

        if (Weapons.Count >= maxWeapons)
        {
            HoldingWeapon holdingWeapon = FindObjectOfType<HoldingWeapon>();
            int currentIndex = holdingWeapon.currentIndex;
            DropWeapon(currentIndex);
            Weapons.Insert(currentIndex, item);

            OnWeaponChanged?.Invoke(currentIndex);
            return;
        }

        Weapons.Add(item);
        int newIndex = Weapons.Count - 1;
        OnWeaponChanged?.Invoke(newIndex);
    }

    public void DropWeapon(int index)
    {
        if (index < 0 || index >= Weapons.Count) return;

        Item item = Weapons[index];

        Instantiate(item.itemData.WorldPrefab, this.transform.position, Quaternion.identity);

        Weapons.RemoveAt(index);
    }
    #endregion
    #region Consumable
    public void AddConsumable(Item item)
    {
        if (Consumables.Count >= maxConsumables)
        {
            DropConsumable(0);
            Consumables.Insert(0, item);
            return;
        }

        Consumables.Add(item);
    }

    public void DropConsumable(int index)
    {
        if (index < 0 || index >= Consumables.Count) return;
        Item item = Consumables[index];
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
}