using UnityEngine;
using UnityEngine.UI;

public class WeaponPHUD : MonoBehaviour
{
    HoldingItemWatcher holdingItemWatcher;
    Transform holdingItem;
    Image image;
    [SerializeField] private RectTransform frame;
    float imageSclae = 0.75f;

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
        image.color = new Color(1f, 1f, 1f, 1f);
        image.sprite = item.itemData.itemIcon;
        image.preserveAspect = true;
        image.SetNativeSize();
        image.rectTransform.sizeDelta = frame.rect.size/imageSclae;
    }
}
