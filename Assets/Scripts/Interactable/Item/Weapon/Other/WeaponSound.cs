using UnityEngine;

public class WeaponSound : MonoBehaviour
{
    [Header("Scriptable Object Data")]
    [SerializeField] protected bool useSOData = true;

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
                Debug.LogWarning("Dont have onAttackSoundID in SOData");
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
    public void PlaySoundOnAttack()
    {
        SoundManager.Instance.PlaySFX(soundID,transform.position);
    }
}
