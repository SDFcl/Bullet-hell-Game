using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData itemData;

    public Item(ItemData data)
    {
        itemData = data;
    }

    public void Use(GameObject user, List<Item> Inventory)
    {
        // Check if this consumable can be used
        if (itemData.consumableType != ConsumableType.Useable)
        {
            Debug.LogWarning($"[Item.Use] Cannot use {itemData.itemName} - it's not a Useable consumable.");
            return;
        }

        foreach (var effect in itemData.effects)
        {
            effect.Apply(user);
        }
        Inventory.Remove(this);
    }
}
