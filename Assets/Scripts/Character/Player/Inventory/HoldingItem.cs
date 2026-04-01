using UnityEngine;

public class HoldingItem : MonoBehaviour
{
    public GameObject DropPosition;

    [SerializeField]
    private Inventory Inventory;

    public GameObject currentItem;
    public int currentIndex = 0;

    private void Awake()
    {
        if (DropPosition == null)
        {
            DropPosition = this.gameObject; // กำหนด DropPosition เป็นตัวเองถ้าไม่ได้กำหนดใน Inspector
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            HoldItem(1); // เลื่อนขวา
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            HoldItem(-1); // เลื่อนซ้าย
        }
    }

    public void SetHoldingItem(int index)
    {
        // ลบของเก่าก่อน
        if (currentItem != null)
        {
            Destroy(currentItem);
        }

        currentIndex = index;

        currentItem = Instantiate(
            Inventory.items[currentIndex].itemData.HoldingPrefab,
            transform
        );
    }

    public void HoldItem(int value)
    {
        if (Inventory.items.Count == 0)
        {
            Debug.Log("No items in inventory to hold.");
            return;
        }

        int count = Inventory.items.Count;

        int Index = (currentIndex + value + count) % count;
        int startIndex = Index;

        while (Inventory.items[Index].itemData == null)
        {
            Index = (Index + value + count) % count;

            if (Index == startIndex)
            {
                Debug.Log("No valid items to hold.");
                return;
            }
        }

        if (currentIndex == Index) return;

        // 🔥 แค่ลบของเก่า (ไม่ drop)
        if (currentItem != null)
        {
            Destroy(currentItem);
        }

        currentIndex = Index;

        currentItem = Instantiate(
            Inventory.items[currentIndex].itemData.HoldingPrefab,
            transform
        );
    }

    public void DropCurrentItem()
    {
        if (currentItem == null) return;

        ItemData data = currentItem.GetComponent<ItemPickup>().itemData;

        // สร้างของในโลก
        Instantiate(data.WorldPrefab, DropPosition.transform.position, Quaternion.identity);

        // ลบออกจาก inventory
        Inventory.items[currentIndex].itemData = null;

        Destroy(currentItem);
        currentItem = null;
    }


}
