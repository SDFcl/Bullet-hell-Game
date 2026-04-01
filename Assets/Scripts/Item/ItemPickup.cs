using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData itemData;

    public void Pickup(Inventory inventory)
    {
        if (itemData != null && inventory != null)
        {
            Item newItem = new Item(itemData);
            inventory.AddItem(newItem);
            Destroy(gameObject); // ทำลอบวัตถุในโลกหลังจากหยิบขึ้นมา
        }
        else
        {
            Debug.LogWarning("ItemData หรือ Inventory ไม่ถูกกำหนดสำหรับการหยิบไอเท็มนี้.");
        }
    }
}
