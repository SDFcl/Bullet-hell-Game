using System;
using UnityEngine;

public class ItemPickup : MonoBehaviour, IPickable, ICollectEvent
{
    public ItemData itemData;
    public event Action<GameObject> OnCollected;

    public ItemData GetItemData()
    {
        return itemData;
    }

    public void Interact(GameObject player)
    {
        Inventory inventory = player.GetComponentInChildren<Inventory>();
        Pickup(inventory);
    }

    public void Pickup(Inventory inventory)
    {
        if (itemData == null || inventory == null)
        {
            Debug.Log("itemData == null"); 
            return;
        } 

        InventoryItem newItem = new InventoryItem(itemData);

        if (itemData.itemType == ItemType.Weapon)
        {
            if (inventory.AddWeapon(newItem))
            {
                OnCollected?.Invoke(gameObject);
                Destroy(gameObject);
            }
        }
        else if (itemData.itemType == ItemType.Consumable)
        {
            if (itemData.consumableType == ConsumableType.passive)
            {
                // สำหรับ passive: Apply effect ตอนเก็บ
                inventory.AddConsumable(newItem);
                ApplyAllEffects(inventory.gameObject);
            }
            else
            {
                inventory.AddConsumable(newItem);
            }

            OnCollected?.Invoke(gameObject);
            Destroy(gameObject);
        }
    }

    // ฟังก์ชันใหม่: ใช้ Apply effect ทั้งหมด
    private void ApplyAllEffects(GameObject target)
    {
        if (itemData == null) return;
        Inventory inventory = target.GetComponentInChildren<Inventory>();
        if (inventory != null)
        {
            ItemData existingItem = inventory.Consumables[0].itemData;
            if (existingItem != itemData)
            {
                foreach (var effect in existingItem.effects)
                {
                    effect.IsActive = false;
                    Debug.Log($"[ItemPickup] Deactivated existing passive item: {existingItem.itemName}");
                }
                Debug.LogWarning($"[ItemPickup] Inventory already has a different passive item: {existingItem.itemName}. Cannot apply effects of {itemData.itemName}.");
            }
        }

        foreach (var effect in itemData.effects)
        {
            effect.Apply(target);
            Debug.Log($"[ItemPickup] Applied passive effect: {itemData.itemName}");
        }
    }
}