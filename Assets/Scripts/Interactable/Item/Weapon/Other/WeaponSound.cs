using UnityEngine;

public class WeaponSound : MonoBehaviour
{

    [Header("Default Setting")]
    [SerializeField] private bool useAnimationEvent = false;
    [SerializeField] SoundID soundID;
    private IWeapon weapon;
    void Awake()
    {
        weapon = GetComponent<IWeapon>();
            
    }
    void OnEnable()
    {
        if(!useAnimationEvent)
        weapon.OnAttack += PlaySoundOnAttack;
    }
    void OnDisable()
    {
        if(!useAnimationEvent)
        weapon.OnAttack -= PlaySoundOnAttack;
    }
    void Start()
    {
        if(weapon != null && weapon.WeaponDataSO != null && weapon.WeaponDataSO.onAttackSoundID != null)
        {
            soundID = weapon.WeaponDataSO.onAttackSoundID;
        }
    }
    public void PlaySoundOnAttack()
    {
        SoundManager.Instance.PlaySFX(soundID,transform.position);
    }
}
