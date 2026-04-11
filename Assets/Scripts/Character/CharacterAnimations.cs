using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    protected Animator animator;
    private IMovement movement;
    private Health health;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<IMovement>();
        health = GetComponent<Health>();
    }

    protected virtual void OnEnable()
    {
        health.OnDead += OnDeadAnimation;
    }

    protected virtual void OnDisable()
    {
        health.OnDead -= OnDeadAnimation;
    }

    protected virtual void Update()
    {
        animator.SetFloat("Velocity", movement.GetMoveInput().magnitude);
    }
    protected virtual void OnDeadAnimation()
    {
        animator.SetBool("IsDead",health.IsDead);
    }
}
