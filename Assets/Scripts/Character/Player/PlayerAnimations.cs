using UnityEngine;

public class PlayerAnimations : CharacterAnimations
{
    private Dodge dodge;
    private PlayerHealth playerHealth;

    protected override void Awake()
    {
        base.Awake();
        dodge = GetComponent<Dodge>();
        playerHealth = GetComponent<PlayerHealth>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        dodge.OnDodge += TriggerDodgeAnimation;
        playerHealth.OnIFrame += TriggerIFrameAnimation;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        dodge.OnDodge -= TriggerDodgeAnimation;
        playerHealth.OnIFrame -= TriggerIFrameAnimation;
    }
    public void TriggerDodgeAnimation()
    {
        animator.SetTrigger("Dodge");
    }

    public void TriggerIFrameAnimation(bool OnIFrame)
    {
        animator.SetBool("OnIFrame",OnIFrame);
    }
}
