using UnityEngine;

public class ProjectileHitSound : MonoBehaviour
{ 
    [Header("Default Setting")]
    [SerializeField] private SoundID soundID;

    private ProjectileHit projectileHit;

    void Awake()
    {
        projectileHit = GetComponent<ProjectileHit>();
    }

    void OnEnable()
    {
        if (projectileHit == null)
            return;

        projectileHit.OnSpawn += SetReference;
        projectileHit.OnHit += PlaySoundOnHit;
    }

    void OnDisable()
    {
        if (projectileHit == null)
            return;

        projectileHit.OnSpawn -= SetReference;
        projectileHit.OnHit -= PlaySoundOnHit;
    }

    void SetReference()
    {
        if (projectileHit != null && projectileHit.WeaponDataSO != null && projectileHit.WeaponDataSO.onHitSoundID != null)
        {
            soundID = projectileHit.WeaponDataSO.onHitSoundID;
        }   
    }

    void PlaySoundOnHit()
    {
        if (soundID == null)
            return;

        SoundManager.Instance.PlaySFX(soundID, transform.position);
    }
}
