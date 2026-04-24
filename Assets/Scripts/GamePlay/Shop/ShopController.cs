using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShopController : MonoBehaviour
{
    public RandomMultipleItemTypes randomItem;
    public RandomPrice randomPrice;

    public ListRateDropItem listHeart;
    public RandomPrice randomPriceHeart;

    public List<Transform> ItemSlot = new List<Transform>();


    private void Awake()
    {
        foreach (Transform slot in ItemSlot)
        {
            if (slot == ItemSlot[2])
            {
                GameObject heartItem = listHeart.Normal[0];
                Debug.Log("Spawning heart item: " + heartItem.gameObject.name + " in slot: " + slot.name);
                GameObject heartItemIns = Instantiate(heartItem, slot.position, Quaternion.identity, slot);
                if (heartItemIns.TryGetComponent(out Collider2D collider2D))
                {
                    collider2D.enabled = false;
                }
                if (slot.gameObject.TryGetComponent(out SlotSellItem slotSellItem))
                {
                    slotSellItem.SetItem(heartItemIns);
                    slotSellItem.Price = randomPrice.GetRandomInt(RareType.Normal);
                }
                return;
            }
            else 
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
}
