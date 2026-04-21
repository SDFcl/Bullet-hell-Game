using UnityEngine;

public class WeaponSound : MonoBehaviour
{
    [Header("Scriptable Object Data")]
    [SerializeField] protected bool useSOData = false;

    [Header("Default Setting")]
    [SerializeField] private bool useAnimationEvent = false;
    [SerializeField] SoundID soundID;
    private IWeapon weapon;
    void Awake()
    {
        weapon = GetComponent<IWeapon>();
        if (useSOData)
        {
            if(weapon.WeaponDataSO.onAttackSoundID != null)
            {
                soundID = weapon.WeaponDataSO.onAttackSoundID;
            }
            else
            {
                Debug.LogWarning(
                $"Dont have onAttackSoundID in SOData | " +
                $"object={name} | " +
                $"instance={GetInstanceID()} | " +
                $"weaponData={(weapon != null && weapon.WeaponDataSO != null ? weapon.WeaponDataSO.name : "NULL")}");

            }
        }
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
        
    }
    public void PlaySoundOnAttack()
    {
        SoundManager.Instance.PlaySFX(soundID,transform.position);
    }
}
