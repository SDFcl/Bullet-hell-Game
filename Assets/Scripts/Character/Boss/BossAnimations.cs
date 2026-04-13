using UnityEngine;

public class BossAnimations : CharacterAnimations
{
    private EnemyAttackSelector attackSelector;
    private AnimationEvent animEvent;

    protected override void Awake()
    {
        base.Awake();
        attackSelector = GetComponent<EnemyAttackSelector>();
        animEvent = GetComponent<AnimationEvent>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        attackSelector.OnSelected += SetIndexSelector;
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
    private void HandlePrepareAttack()
    {
        animator.SetTrigger("PrepareAttack");
    }
}
