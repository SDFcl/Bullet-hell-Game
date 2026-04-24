using UnityEngine;

public class SFXOnBuyItem : MonoBehaviour
{
    [SerializeField] private SoundID soundID;
    private SlotSellItem slotSellItem;
    void Awake()
    {
        slotSellItem = GetComponent<SlotSellItem>();
    }
    void OnEnable()
    {
        slotSellItem.OnSuccess += PlaySFX;
    }
    void OnDisable()
    {
        slotSellItem.OnSuccess -= PlaySFX;
    }
    void PlaySFX()
    {
        SoundManager.Instance.PlaySFX(soundID,transform.position);
    }
}
