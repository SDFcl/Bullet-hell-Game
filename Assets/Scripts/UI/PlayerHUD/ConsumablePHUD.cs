using UnityEngine;
using UnityEngine.UI;

public class ConsumablePHUD : MonoBehaviour
{
    Inventory inventory;
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
            inventory.OnConsumableChanged += ChangeImage;
            ChangeImage();
        }
    }

    void OnDisable()
    {
        if (inventory != null)
            inventory.OnConsumableChanged -= ChangeImage;
    }

    void ChangeImage()
    {
        Debug.Log("trigger event");
        if (inventory == null || image == null)
            return;

        if (inventory.Consumables.Count == 0)
        {
            image.sprite = null;
            return;
        }

        image.sprite = inventory.Consumables[0].itemData.itemIcon;
    }
}
