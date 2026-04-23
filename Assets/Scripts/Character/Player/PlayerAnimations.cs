using UnityEngine;
using System.Collections;

public class PlayerAnimations : CharacterAnimations
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float blinkInterval = 0.08f;

    private Coroutine blinkRoutine;

    private Dodge dodge;
    private PlayerHealth playerHealth;
    private IFrameController iframeController;

    protected override void Awake()
    {
        base.Awake();

        dodge = GetComponent<Dodge>();
        playerHealth = GetComponent<PlayerHealth>();
        iframeController = GetComponent<IFrameController>();

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
        if (blinkRoutine != null)
        {
            StopCoroutine(blinkRoutine);
            blinkRoutine = null;
        }

        if (spriteRenderer != null)
            spriteRenderer.enabled = true;

    }
    public void TriggerDodgeAnimation()
    {
        animator.SetTrigger("Dodge");
    }

    public void HandleIFrameBlink()
    {
        if (blinkRoutine != null)
        {
            StopCoroutine(blinkRoutine);
            blinkRoutine = null;
        }

        if (iframeController.IsDamageBlocked)
        {
            blinkRoutine = StartCoroutine(BlinkRoutine());
            return;
        }

        spriteRenderer.enabled = true;
    }

    private IEnumerator BlinkRoutine()
    {
        while (iframeController.IsDamageBlocked)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
        }

        spriteRenderer.enabled = true;
        blinkRoutine = null;
    }

}
