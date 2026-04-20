using UnityEngine;

public class WeaponHitSound : MonoBehaviour
{
    [SerializeField] private SoundID soundID;
    private Hitbox hitbox;
    

    void Awake()
    {
        hitbox = GetComponent<Hitbox>();
    }
    void OnEnable()
    {
        hitbox.OnHit += PlaySoundOnHit;
    }
    void OnDisable()
    {
        hitbox.OnHit -= PlaySoundOnHit;
    }
    void PlaySoundOnHit()
    {
        SoundManager.Instance.PlaySFX(soundID,transform.position);
    }
}
