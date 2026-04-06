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
            inventory.AddWeapon(newItem);
        if (itemData.itemType == ItemType.Consumable)
            inventory.AddConsumable(newItem);
        Destroy(gameObject);
    }
}