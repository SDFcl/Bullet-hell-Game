using UnityEngine;

public class HitboxHitSound : MonoBehaviour
{

    [Header("Default Setting")]
    [SerializeField] private SoundID soundID;

    
    private Hitbox hitbox;

    private void Awake()
    {
        hitbox = GetComponent<Hitbox>();
    }

    private void OnEnable()
    {
        if (hitbox != null)
            hitbox.OnHit += PlaySoundOnHit;
    }

    private void OnDisable()
    {
        if (hitbox != null)
            hitbox.OnHit -= PlaySoundOnHit;
    }

    private void Start()
    {
        if (hitbox.WeaponDataSO.onHitSoundID != null && hitbox != null)
        {
             soundID = hitbox.WeaponDataSO.onHitSoundID;
        }    
    }

    private void PlaySoundOnHit()
    {
        if (soundID == null)
            return;

        SoundManager.Instance.PlaySFX(soundID, transform.position);
    }
}
