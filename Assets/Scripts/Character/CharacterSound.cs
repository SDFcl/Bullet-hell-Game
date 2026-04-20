using UnityEngine;

public class CharacterSound : MonoBehaviour
{
    [Header("BaseID")]
    [SerializeField] SoundID hurtID;
    [SerializeField] private bool useDeadAnimationEvent = false;
    [SerializeField] SoundID dieID;
    private Health health;
    protected virtual void Awake()
    {
        health = GetComponent<Health>();
    }
    protected virtual void OnEnable()
    {
        health.Onhit += PlaySoundOnHit;
        if(!useDeadAnimationEvent)
            health.OnDead += PlaySoundOnDead;
    }
    protected virtual void OnDisable()
    {
        health.Onhit -= PlaySoundOnHit;
        if(!useDeadAnimationEvent)
            health.OnDead -= PlaySoundOnDead;
    }

    void PlaySoundOnHit()
    {
        SoundManager.Instance.PlaySFX(hurtID,transform.position);
    }
    public void PlaySoundOnDead()
    {
        SoundManager.Instance.PlaySFX(dieID,transform.position);
    }
}
