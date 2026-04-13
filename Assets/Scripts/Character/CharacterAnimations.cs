using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    protected Animator animator;
    private IMovement movement;
    private Attack attack;
    private Health health;

    protected virtual void Awake()
    {
        attack = GetComponent<Attack>();
        animator = GetComponent<Animator>();
        movement = GetComponent<IMovement>();
        health = GetComponent<Health>();
    }

    protected virtual void OnEnable()
    {
        health.OnDead += OnDeadAnimation;
        attack.OnAttacked += OnAttackAnimation;
    }

    protected virtual void OnDisable()
    {
        health.OnDead -= OnDeadAnimation;
        attack.OnAttacked -= OnAttackAnimation;
    }

    protected virtual void Update()
    {
        animator.SetFloat("Velocity", movement.GetMoveInput().magnitude);
    }
    protected virtual void OnDeadAnimation()
    {
        animator.SetTrigger("Dead");
    }
    protected virtual void OnAttackAnimation()
    {
        animator.SetTrigger("Attack");
    }
}
