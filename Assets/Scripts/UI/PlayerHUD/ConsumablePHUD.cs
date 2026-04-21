using UnityEngine.UI;
using UnityEngine;

public class ConsumablePHUD : MonoBehaviour
{
    InventoryItem currentConsumable;
    Inventory inventory;
    Image image;
    void Awake()
    {
        if(inventory = null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            inventory = player.GetComponent<Inventory>();
        }
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
    private void RefreshCurrentConsumable()
    {
        UnsubscribeCurrentConsumable();

        if (inventory == null)
            return;

        if (inventory.Consumables.Count == 0)
            return;

        currentConsumable = inventory.Consumables[0];
        currentConsumable.OnUse += ChangeImage;
    }

    private void UnsubscribeCurrentConsumable()
    {
        if (currentConsumable == null)
            return;

        currentConsumable.OnUse -= ChangeImage;
        currentConsumable = null;
    }

    void ChangeImage()
    {
        image.sprite = currentConsumable.itemData.itemIcon;
    }
}
