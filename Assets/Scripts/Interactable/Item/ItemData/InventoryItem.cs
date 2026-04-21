using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public Action OnUse;
    public ItemData itemData;

    public InventoryItem(ItemData data)
    {
        itemData = data;
    }

    public void Use(GameObject user, List<InventoryItem> inventory)
    {
        if (itemData == null)
            return;

        if (itemData.consumableType != ConsumableType.Useable)
        {
            Debug.LogWarning($"[InventoryItem.Use] Cannot use {itemData.itemName} - it's not a Useable consumable.");
            return;
        }

        foreach (var effect in itemData.effects)
        {
            effect.Apply(user);
        }

        OnUse?.Invoke();
        inventory.Remove(this);
    }
}
