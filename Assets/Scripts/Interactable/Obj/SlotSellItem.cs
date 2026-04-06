using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SlotSellItem : InteractiveSlotSellItem
{
    [Header("Slot Sell Item Settings")]
    [SerializeField] private bool IsSold = false;
    [SerializeField] private GameObject ItemGameObject;

    private BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
    }

    public void SetItem(GameObject item)
    {
        ItemGameObject = item;
    }

    public override string GetInteractionName()
    {
        return "Sell Item";
    }

    protected override void ExecuteInteraction(GameObject player)
    {
        if (IsSold) return;

        if (player.TryGetComponent(out Inventory inventory) && ItemGameObject.TryGetComponent(out ItemPickup itemPickup))
        {
            ItemData itemData = itemPickup.GetItemData();
            if (inventory.Coins >= itemData.Price)
            {
                inventory.Coins -= itemData.Price;
                IsSold = true;
                if (ItemGameObject.TryGetComponent(out Collider2D itemCollider))
                {
                    itemCollider.enabled = true; // Disable collider after selling
                }
                boxCollider.enabled = false; // Disable collider after selling
                Debug.Log($"[SlotSellItem] Sold {itemData.itemName} for {itemData.Price} coins.");
            }
            else
            {
                Debug.Log($"[SlotSellItem] Not enough coins to sell {itemData.itemName}. Required: {itemData.Price}, Available: {inventory.Coins}");
            }
        }
        else
        {
            Debug.LogWarning("[SlotSellItem] Player inventory or item data is missing. Cannot execute sell interaction.");
        }
    }
}
