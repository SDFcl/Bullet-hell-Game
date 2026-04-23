using TMPro;
using UnityEngine;

public class SlotShopOnFogus : OnFogus
{
    public GameObject GameObject;
    public SlotSellItem slotSellItem;

    private void Start()
    {
        GameObject.SetActive(false);
    }

    public override void Execute()
    {
        GameObject.SetActive(onFogus);
        TextMeshPro textMeshPro = GameObject.GetComponentInChildren<TextMeshPro>();
        Debug.Log($"[SlotShopOnFogus] Updating UI text for {slotSellItem.ItemGameObject.name} with price {slotSellItem.Price} coins.");
        if (textMeshPro != null)
        {
            ItemData itemData = slotSellItem.ItemGameObject.GetComponent<ItemPickup>().GetItemData();
            Debug.Log($"[SlotShopOnFogus] Retrieved item data: {itemData?.itemName ?? "null"}");
            if (itemData != null)
            {
                textMeshPro.text = $"<voffset=6><sprite name=\"Coin UI_0\"></voffset> {slotSellItem.Price} {itemData.itemName}";
            }
            else
            {
                Debug.LogWarning("[SlotShopOnFogus] Item data is missing. Cannot update UI text.");
            }
        }
    }
}
