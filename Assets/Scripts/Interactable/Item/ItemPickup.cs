using UnityEngine;

public class ItemPickup : MonoBehaviour, IPickable
{
    public ItemData itemData;

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
        if (itemData == null || inventory == null) return;

        Item newItem = new Item(itemData);

        // Depending on the item type, add it to the appropriate inventory list
        if (itemData.itemType == ItemType.Weapon)
        {
            inventory.AddWeapon(newItem);
        }
        else if (itemData.itemType == ItemType.Consumable)
        {
            // Check if it's a passive effect or useable
            if (itemData.consumableType == ConsumableType.passive)
            {
                // Apply passive effect immediately without storing
                GameObject player = inventory.GetComponentInParent<PlayerController>()?.gameObject ?? inventory.gameObject;
                foreach (var effect in itemData.effects)
                {
                    effect.Apply(player);
                }
                Debug.Log($"[ItemPickup] Applied passive effect: {itemData.itemName}");
            }
            else
            {
                // Useable consumable - store in inventory
                inventory.AddConsumable(newItem);
            }
        }
        Destroy(gameObject);
    }
}