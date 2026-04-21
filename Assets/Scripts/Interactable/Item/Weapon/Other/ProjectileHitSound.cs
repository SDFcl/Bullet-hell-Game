using UnityEngine;

public class ProjectileHitSound : MonoBehaviour
{ 
    [Header("Default Setting")]
    [SerializeField] private SoundID soundID;

    private bool useSOData = false;
    private ProjectileHit projectileHit;
    void Awake()
    {
        projectileHit = GetComponent<ProjectileHit>();
    }
    void OnEnable()
    {
        projectileHit.OnSpawn += SetReference;
        projectileHit.OnHit += PlaySoundOnHit;
    }
    void OnDisable()
    {
        projectileHit.OnSpawn -= SetReference;
        projectileHit.OnHit -= PlaySoundOnHit;
    }
    void SetReference()
    {
        if (projectileHit.WeaponDataSO.onHitSoundID != null && projectileHit != null)
        {
             soundID = projectileHit.WeaponDataSO.onHitSoundID;
        }   
    }
    void PlaySoundOnHit()
    {
        SoundManager.Instance.PlaySFX(soundID, transform.position);
    }

}
