using System;
using UnityEngine;

public class ItemPickup : MonoBehaviour, IPickable,ICollectEvent
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
        //Debug.Log("interact"); 
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
                    GameObject player = inventory.GetComponentInParent<PlayerController>()?.gameObject ?? inventory.gameObject;

                    foreach (var effect in itemData.effects)
                    {
                        effect.Apply(player);
                    }

                    inventory.AddConsumable(newItem);
                    Debug.Log($"[ItemPickup] Applied passive effect: {itemData.itemName}");
                }
                else
                {
                    inventory.AddConsumable(newItem);
                }
                OnCollected?.Invoke(gameObject);
                Destroy(gameObject);
            }
    }

}