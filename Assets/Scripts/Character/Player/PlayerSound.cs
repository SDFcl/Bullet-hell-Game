using UnityEngine;

public class PlayerSound : CharacterSound
{
    [Header("Player Setting")]
    [SerializeField] private bool useDodgeAnimationEvent = false;
    [SerializeField] private SoundID DodgeID;
    [SerializeField] private SoundID UseConsumable;
    [SerializeField] private SoundID UseSpecialAbility;
    [SerializeField] private SoundID OnSpeAbililyActive;

    private Dodge playerDodge;
    private Inventory playerInventory;
    private InventoryItem currentConsumable;
    private SpecialAbility specialAbility;

    protected override void Awake()
    {
        base.Awake();

        playerDodge = GetComponent<Dodge>();
        playerInventory = GetComponent<Inventory>();
        specialAbility = GetComponent<SpecialAbility>();
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

        if(specialAbility != null)
        {
            specialAbility.OnActive += PlayerSoundOnSpecialAbility;
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        if (!useDodgeAnimationEvent && playerDodge != null)
            playerDodge.OnDodge -= PlaySoundOnRoll;

        if (playerInventory != null)
        {
            playerInventory.OnConsumableChanged -= RefreshCurrentConsumable;
            UnsubscribeCurrentConsumable();
        }   

        if(specialAbility != null)
        {
            specialAbility.OnActive -= PlayerSoundOnSpecialAbility;
        }
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
    public void PlayerSoundOnSpecialAbility(float duration)
    {
        SoundManager.Instance.PlaySFX(UseSpecialAbility, transform.position);
        SoundManager.Instance.PlayLoopSFX(OnSpeAbililyActive, transform.position,duration);
    }
}
