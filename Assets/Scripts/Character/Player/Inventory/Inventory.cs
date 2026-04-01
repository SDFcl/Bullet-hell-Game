using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Settings")]
    public int maxItems = 5;

    [Header("References")]
    public HoldingItem holdingItem;
    public List<Item> items = new List<Item>();


    public void AddItem(Item item)
    {
        // 🔹 กรณี inventory ว่าง
        if (items.Count == 0)
        {
            items.Add(item);
            Debug.Log("Holding item : " + item.itemData.itemName);
            holdingItem.SetHoldingItem(0);
            return;
        }

        // 🔹 กรณีเต็ม
        if (items.Count >= maxItems)
        {
            Debug.Log("Inventory full, replacing current item");

            int index = holdingItem.currentIndex;

            // 1. Drop ของเก่า
            holdingItem.DropCurrentItem();

            // 2. แทนที่ slot เดิม (ไม่ใช่ Add)
            items[index] = item;

            // 3. ถือของใหม่
            holdingItem.SetHoldingItem(index);

            Debug.Log("Holding new item : " + item.itemData.itemName);
            return;
        }

        // 🔹 กรณีปกติ
        items.Add(item);
        Debug.Log("Added item: " + item.itemData.itemName);
    }

    public void RemoveItem(Item item, Vector2 dropPosition)
    {
        if (items.Contains(item))
        {
            GameObject.Instantiate(item.itemData.WorldPrefab, dropPosition, Quaternion.identity);
            items.Remove(item);
            Debug.Log("Removed item: " + item.itemData.itemName);
        }
        else
        {
            Debug.Log("Item not found in inventory: " + item.itemData.itemName);
        }
    }

    /* Debugging method to check inventory contents
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //
            Debug.Log(Equals(items.Count, 0) ? "Inventory is empty." : "Inventory has " + items.Count + " item(s).");
            Debug.Log(items.Count > 0 ? "First item: " + items[0].itemData.itemName : "No items to display.");
        }
    }*/
}
