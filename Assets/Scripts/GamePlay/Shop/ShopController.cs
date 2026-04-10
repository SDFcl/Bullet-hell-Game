using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public RandomItem randomItem;
    public RandomPrice randomPrice;

    public List<Transform> ItemSlot = new List<Transform>();

    private void Awake()
    {
        foreach (Transform slot in ItemSlot)
        {
            GameObject item = randomItem.GetRandomItem();
            Debug.Log("Spawning item: " + item.gameObject.name + " in slot: " + slot.name);
            GameObject itemIns = Instantiate(item, slot.position, Quaternion.identity, slot);
            
            if (itemIns.TryGetComponent(out Collider2D collider2D))
            {
                collider2D.enabled = false;
            }

            if (slot.gameObject.TryGetComponent(out SlotSellItem slotSellItem))
            {
                slotSellItem.SetItem(itemIns);
                ItemData itemData = itemIns.GetComponent<ItemPickup>().GetItemData();
                slotSellItem.Price = randomPrice.GetRandomInt(itemData.rareType);
            }
        }
    }
}
