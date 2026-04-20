using UnityEngine;

public class WeaponAnimaion : MonoBehaviour
{
    protected Animator animator;
    protected IWeapon weapon;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        weapon = GetComponent<IWeapon>();
    }
    protected virtual void OnEnable()
    {
        if (weapon != null)
        {
            weapon.OnAttack += TriggerAttackAnimation;
        }
    }

    protected virtual void OnDestroy()
    {
        if (weapon != null)
        {
            weapon.OnAttack -= TriggerAttackAnimation;
        }
    }

    protected virtual void TriggerAttackAnimation()
    {
        animator.SetTrigger("Attack");
    }
}
