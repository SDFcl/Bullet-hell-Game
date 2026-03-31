using UnityEngine;

public class WeaponAnimaion : MonoBehaviour
{
    private Animator animator;
    private IWeapon weapon;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        weapon = GetComponent<IWeapon>();
    }
    private void OnEnable()
    {
        if (weapon != null)
        {
            weapon.OnAttack += TriggerAttackAnimation;
        }
    }

    private void OnDestroy()
    {
        if (weapon != null)
        {
            weapon.OnAttack -= TriggerAttackAnimation;
        }
    }

    private void TriggerAttackAnimation()
    {
        animator.SetTrigger("Attack");
    }
}
