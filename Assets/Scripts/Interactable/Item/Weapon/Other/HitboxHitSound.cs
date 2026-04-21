using UnityEngine;

public class HitboxHitSound : MonoBehaviour
{

    [Header("Default Setting")]
    [SerializeField] private SoundID soundID;

    private bool useSOData = false;
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
        if(hitbox != null)
        {
            useSOData = hitbox.UseSOData;
            if (useSOData)
            {
                soundID = hitbox.WeaponDataSO.onHitSoundID;
            }     
        } 
    }

    private void PlaySoundOnHit()
    {
        if (soundID == null)
            return;

        SoundManager.Instance.PlaySFX(soundID, transform.position);
    }
}
