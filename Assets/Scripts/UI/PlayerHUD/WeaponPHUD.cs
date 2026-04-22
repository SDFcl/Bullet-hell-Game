using UnityEngine;
using UnityEngine.UI;

public class WeaponPHUD : MonoBehaviour
{
    HoldingItemWatcher holdingItemWatcher;
    GameObject player;
    Image image;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
            holdingItemWatcher = player.GetComponent<HoldingItemWatcher>();

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
        Item item = player.GetComponentInChildren<Item>();
        image.sprite = item.itemData.itemIcon;
    }
}
