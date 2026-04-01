using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;
    private Dodge dodge;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        dodge = GetComponent<Dodge>();
    }

    private void OnEnable()
    {
        if (dodge != null)
        {
            //subscribe
            dodge.OnDodge += TriggerDodgeAnimation;
        }
    }

    private void OnDisable()
    {
        if (dodge != null)
        {
            //unsubscribe
            dodge.OnDodge -= TriggerDodgeAnimation;
        }
    }

    //Play Dodge Animation
    public void TriggerDodgeAnimation()
    {
        animator.SetTrigger("Dodge");
    }
}
