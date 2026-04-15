using UnityEngine;

public class HoldingWeapon : MonoBehaviour
{
    [SerializeField] private Inventory inventory;

    public GameObject currentItem;
    public int currentIndex;

    private int direction = 0;

    private void Awake()
    {
        if (inventory == null)
        {
            inventory = GetComponentInParent<PlayerController>()?.GetComponentInChildren<Inventory>();
        }

        if (inventory == null)
        {
            inventory = GetComponentInParent<Inventory>();
        }

        /// ลองเช็ค item ที่อยู่ใน child ว่ามีไหม ถ้ามีก็ใส่เข้า inventory ไปเลย (กรณีที่มี item อยู่แล้วตอนเริ่มเกม)
        Item item = GetComponentInChildren<Item>();
        if (item != null)
        {
            Debug.Log(GameSession.savedInventory.weapons);
            if(GameSession.savedInventory.weapons != null && GameSession.savedInventory.weapons.Count > 0)
            {
                Debug.Log("HoldingWeapon: Found item in child on Awake, but inventory already has saved weapons. Skipping adding item to inventory: " + item.itemData.itemName);
                return;
            }
            inventory.AddWeapon(item);
            //Debug.Log("HoldingWeapon: Added item to inventory on Awake: " + item.itemData.itemName);
        }
    }

    private void OnEnable()
    {
        if (inventory != null)
        {
            inventory.OnWeaponChanged += SetHoldingWeapon;
        }
        else
        {
            Debug.LogWarning("HoldingWeapon: Inventory reference is missing. Cannot subscribe to OnWeaponChanged.");
        }
    }

    private void OnDisable()
    {
        if (inventory != null)
        {
            inventory.OnWeaponChanged -= SetHoldingWeapon;
        }
    }
    public void SetDirection(int value)
    {
        direction = value;
        SetHoldingWeapon(currentIndex);
    }

    public void SetHoldingWeapon(int index)
    {
        if (inventory == null)
        {
            Debug.LogWarning("HoldingWeapon: Inventory reference is missing. Cannot set holding weapon.");
            return;
        }

         // ถ้าไม่ได้ส่ง index มา ให้ใช้ currentIndex
         //Debug.Log("SetHoldingWeapon called with index: " + index + ", direction: " + direction);

        if (index == currentIndex) 
        {
            int count = inventory.Weapons.Count;

            if (count == 0)
            {
                Debug.Log("No items in inventory to hold.");
                return;
            }

            // ทำให้ index อยู่ใน range ก่อน
            //Debug.Log("Current index before adjustment: " + (index + direction + count) % count);
            index = (index + direction + count) % count;

            int startIndex = index;

            // 🔥 วนหา item ที่ valid
            //Debug.Log("Searching for valid weapon starting at index: " + index);
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