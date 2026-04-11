using UnityEngine;

public class PlayerAnimations : CharacterAnimations
{
    private Dodge dodge;

    protected override void Awake()
    {
        base.Awake();
        dodge = GetComponent<Dodge>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        dodge.OnDodge += TriggerDodgeAnimation;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        dodge.OnDodge -= TriggerDodgeAnimation;
    }
    public void TriggerDodgeAnimation()
    {
        animator.SetTrigger("Dodge");
    }
}
