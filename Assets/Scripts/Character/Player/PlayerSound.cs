using UnityEngine;

public class PlayerSound : CharacterSound
{
    [Header("Player Setting")]
    [SerializeField] private bool useDodgeAnimationEvent = false;
    [SerializeField] private SoundID DodgeID;
    [SerializeField] private SoundID UseConsumable;

    private Dodge playerDodge;
    private Inventory playerInventory;
    private InventoryItem currentConsumable;

    protected override void Awake()
    {
        base.Awake();

        playerDodge = GetComponent<Dodge>();
        playerInventory = GetComponent<Inventory>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        if (!useDodgeAnimationEvent && playerDodge != null)
            playerDodge.OnDodge += PlaySoundOnRoll;

        if (playerInventory != null)
        {
            playerInventory.OnConsumableChanged += RefreshCurrentConsumable;
            RefreshCurrentConsumable();
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        if (!useDodgeAnimationEvent && playerDodge != null)
            playerDodge.OnDodge -= PlaySoundOnRoll;

        if (playerInventory != null)
            playerInventory.OnConsumableChanged -= RefreshCurrentConsumable;

        UnsubscribeCurrentConsumable();
    }

    private void RefreshCurrentConsumable()
    {
        UnsubscribeCurrentConsumable();

        if (playerInventory == null)
            return;

        if (playerInventory.Consumables.Count == 0)
            return;

        currentConsumable = playerInventory.Consumables[0];
        currentConsumable.OnUse += PlaySoundOnUseConsumable;
    }

    private void UnsubscribeCurrentConsumable()
    {
        if (currentConsumable == null)
            return;

        currentConsumable.OnUse -= PlaySoundOnUseConsumable;
        currentConsumable = null;
    }

    public void PlaySoundOnRoll()
    {
        SoundManager.Instance.PlaySFX(DodgeID, transform.position);
    }

    public void PlaySoundOnUseConsumable()
    {
        SoundManager.Instance.PlaySFX(UseConsumable, transform.position);

        // เพราะ item นี้ถูกใช้แล้ว เดี๋ยวมันโดน Remove ออกจาก list
        UnsubscribeCurrentConsumable();
    }
}
