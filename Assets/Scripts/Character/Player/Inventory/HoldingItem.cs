using UnityEngine;

public class HoldingWeapon : MonoBehaviour
{
    [SerializeField] private Inventory inventory;

    public GameObject currentItem;
    public int currentIndex;

    private void OnEnable()
    {
        inventory.OnWeaponChanged += SetHoldingWeapon;
    }

    private void OnDisable()
    {
        inventory.OnWeaponChanged -= SetHoldingWeapon;
    }

    public void SetHoldingWeapon(int index = -2, int direction = 0, bool Check = true)
    {
        if (index == -2) index = currentIndex; // ถ้าไม่ได้ส่ง index มา ให้ใช้ currentIndex
        Debug.Log("SetHoldingWeapon called with index: " + index + ", direction: " + direction + ", Check: " + Check);

        if (Check)
        {
            int count = inventory.Weapons.Count;

            if (count == 0)
            {
                Debug.Log("No items in inventory to hold.");
                return;
            }

            // ทำให้ index อยู่ใน range ก่อน
            index = (index + direction + count) % count;

            int startIndex = index;

            // 🔥 วนหา item ที่ valid
            while (inventory.Weapons[index].itemData == null)
            {
                index = (index + direction + count) % count;

                // ถ้าวนครบแล้วไม่เจอ
                if (index == startIndex)
                {
                    Debug.Log("No valid weapon to hold.");
                    return;
                }
            }

            // ถ้าเป็นตัวเดิม ไม่ต้องทำอะไร
            if (currentIndex == index) return;
        }

        // ลบของเก่า
        if (currentItem != null)
        {
            Destroy(currentItem);
        }

        currentIndex = index;

        Item item = inventory.Weapons[index];

        currentItem = Instantiate(item.itemData.HoldingPrefab, transform);
        Debug.Log("Holding weapon: " + item.itemData.itemName + " at index " + index);
    }

    public void HoldWeapon(int value)
    {
        if (inventory.Weapons.Count == 0) 
        { 
            Debug.Log("No items in inventory to hold.");
            return; 
        }

        // 🔥 ใช้ modulo เพื่อให้มันวนรอบได้ และเช็คว่ามันมี item ไหม ถ้าไม่มีก็ข้ามไปเรื่อยๆ จนกว่าจะเจอ หรือถ้าเจอกลับมาที่เดิมก็หยุด
        int count = inventory.Weapons.Count;
        int Index = (currentIndex + value + count) % count;
        int startIndex = Index;
        while (inventory.Weapons[Index].itemData == null) 
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
        currentItem = Instantiate( inventory.Weapons[currentIndex].itemData.HoldingPrefab, transform ); 
    }
}