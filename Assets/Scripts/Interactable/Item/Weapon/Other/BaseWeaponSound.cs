using UnityEngine;

public class BaseWeaponSound : MonoBehaviour
{
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
    public void PlaySoundOnAttack()
    {
        SoundManager.Instance.PlaySFX(soundID,transform.position);
    }
}
