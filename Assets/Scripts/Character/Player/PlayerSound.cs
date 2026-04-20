using UnityEngine;

public class PlayerSound : CharacterSound
{
    [Header("Player Setting")]
    [SerializeField] private bool useDodgeAnimationEvent = false;
    [SerializeField] private SoundID DodgeID;
    private Dodge playerDodge;
    protected override void Awake()
    {
        base.Awake();
        playerDodge = GetComponent<Dodge>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (!useDodgeAnimationEvent)
            playerDodge.OnDodge += PlaySoundOnRoll;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        if (!useDodgeAnimationEvent)
            playerDodge.OnDodge -= PlaySoundOnRoll;
    }

    public void PlaySoundOnRoll()
    {
        SoundManager.Instance.PlaySFX(DodgeID,transform.position);
    }
}
