using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Weapons")]
    public List<Item> Weapons = new List<Item>();
    public int maxWeapons = 5;

    [Header("Consumables")]
    public List<Item> Consumables = new List<Item>();
    public int maxConsumables = 1;

    // 🔥 Event
    public Action<int,int,bool> OnWeaponChanged;

    public void AddWeapon(Item item)
    {
        if (Weapons.Count == 0)
        {
            Weapons.Add(item);
            OnWeaponChanged?.Invoke(0, 0, false);
            Debug.Log("Added weapon: " + item.itemData.itemName + " at index 0");
            return;
        }

        if (Weapons.Count >= maxWeapons)
        {
            HoldingWeapon holdingWeapon = FindObjectOfType<HoldingWeapon>();
            int currentIndex = holdingWeapon.currentIndex;
            DropWeapon(currentIndex);
            Weapons.Insert(currentIndex, item);

            OnWeaponChanged?.Invoke(currentIndex, 0, false);
            return;
        }

        Weapons.Add(item);
        int newIndex = Weapons.Count - 1;
        OnWeaponChanged?.Invoke(newIndex, 0, false);
    }

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

    public void DropWeapon(int index)
    {
        if (index < 0 || index >= Weapons.Count) return;

        Item item = Weapons[index];

        Instantiate(item.itemData.WorldPrefab, this.transform.position, Quaternion.identity);

        Weapons.RemoveAt(index);
    }

    public void DropConsumable(int index)
    {
        if (index < 0 || index >= Consumables.Count) return;
        Item item = Consumables[index];
        Instantiate(item.itemData.WorldPrefab, this.transform.position, Quaternion.identity);
        Consumables.RemoveAt(index);
    }
}