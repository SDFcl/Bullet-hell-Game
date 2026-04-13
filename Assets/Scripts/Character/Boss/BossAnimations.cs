using UnityEngine;

public class BossAnimations : CharacterAnimations
{
    private EnemyAttackSelector attackSelector;
    private Attack attack;
    private AimPivot2D aimPivot2D;

    protected override void Awake()
    {
        base.Awake();
        attackSelector = GetComponent<EnemyAttackSelector>();
        attack = GetComponent<Attack>();
        aimPivot2D = GetComponent<AimPivot2D>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        attackSelector.OnSelected += SetIndexSelector;
        attack.OnAttacked += PlayAttackAfterReset;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        attackSelector.OnSelected -= SetIndexSelector;
    }
    public void SetIndexSelector(int index)
    {
        animator.SetInteger("AttackSelected",index);
    }
    public void PlayAttackAfterReset()
    {
        aimPivot2D.ResetRotation(()=> {animator.SetTrigger("Attack");});
    }
}
