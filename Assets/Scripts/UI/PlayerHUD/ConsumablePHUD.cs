using UnityEngine;
using UnityEngine.UI;

public class ConsumablePHUD : MonoBehaviour
{
    Inventory inventory;
    InventoryItem currentConsumable;
    Image image;

    void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
            inventory = player.GetComponent<Inventory>();

        image = GetComponent<Image>();
    }

    void OnEnable()
    {
        if (inventory != null)
        {
            inventory.OnConsumableChanged += RefreshCurrentConsumable;
            RefreshCurrentConsumable();
        }
    }

    void OnDisable()
    {
        if (inventory != null)
            inventory.OnConsumableChanged -= RefreshCurrentConsumable;

        UnsubscribeCurrentConsumable();
    }

    void RefreshCurrentConsumable()
    {
        UnsubscribeCurrentConsumable();

        if (inventory == null || image == null)
            return;

        if (inventory.Consumables.Count == 0)
        {
            image.sprite = null;
            return;
        }

        currentConsumable = inventory.Consumables[0];
        
        image.color = Color.white;
        image.sprite = currentConsumable.itemData.itemIcon;

        currentConsumable.OnUse += ClearImage;
    }

    void UnsubscribeCurrentConsumable()
    {
        if (currentConsumable == null)
            return;

        currentConsumable.OnUse -= ClearImage;
        currentConsumable = null;
    }

    void ClearImage()
    {
        image.color = new Color(1f, 1f, 1f, 0f);
        UnsubscribeCurrentConsumable();
    }
}
