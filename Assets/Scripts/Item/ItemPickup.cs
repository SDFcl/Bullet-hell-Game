using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData itemData;


    public void Pickup(Inventory inventory)
    {
        if (itemData == null || inventory == null) return;

        Item newItem = new Item(itemData);
        if (itemData.itemType == ItemType.Weapon)
            inventory.AddWeapon(newItem);
        if (itemData.itemType == ItemType.Consumable)
            inventory.AddConsumable(newItem);
        Destroy(gameObject);
    }
}