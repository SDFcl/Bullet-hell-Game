using UnityEngine;
using System.Collections;

public class PlayerAnimations : CharacterAnimations
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float blinkInterval = 0.08f;

    private Coroutine blinkRoutine;

    private Dodge dodge;
    private PlayerHealth playerHealth;

    protected override void Awake()
    {
        base.Awake();

        dodge = GetComponent<Dodge>();
        playerHealth = GetComponent<PlayerHealth>();

        if (spriteRenderer == null)
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        dodge.OnDodge += TriggerDodgeAnimation;
        playerHealth.OnIFrame += HandleIFrameBlink;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        dodge.OnDodge -= TriggerDodgeAnimation;
        playerHealth.OnIFrame -= HandleIFrameBlink;
    }
    public void TriggerDodgeAnimation()
    {
        animator.SetTrigger("Dodge");
    }

    public void HandleIFrameBlink(bool isOnIFrame)
{
    if (blinkRoutine != null)
    {
        StopCoroutine(blinkRoutine);
        blinkRoutine = null;
    }

    if (isOnIFrame)
    {
        blinkRoutine = StartCoroutine(BlinkRoutine());
        return;
    }

    spriteRenderer.enabled = true;
}

    private IEnumerator BlinkRoutine()
    {
        while (true)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
