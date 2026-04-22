using UnityEngine;
using UnityEngine.UI;

public class WeaponPHUD : MonoBehaviour
{
    HoldingItemWatcher holdingItemWatcher;
    Transform holdingItem;
    Image image;

    void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            holdingItem = player.transform.Find("HoldingItem");

            if (holdingItem != null)
                holdingItemWatcher = holdingItem.GetComponent<HoldingItemWatcher>();
        }

        image = GetComponent<Image>();
    }
    void Start()
    {
        ChangeWeaponImage();
    }

    void OnEnable()
    {
        if (holdingItemWatcher != null)
        {
            holdingItemWatcher.OnHoldingItemChanged += ChangeWeaponImage;
        }
    }

    void OnDisable()
    {
        if (holdingItemWatcher != null)
            holdingItemWatcher.OnHoldingItemChanged -= ChangeWeaponImage;
    }

    void ChangeWeaponImage()
    {
        Item item = holdingItem.GetComponentInChildren<Item>();
        image.sprite = item.itemData.itemIcon;
    }
}
