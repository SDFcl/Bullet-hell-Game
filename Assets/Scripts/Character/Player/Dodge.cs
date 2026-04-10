using UnityEngine;
using System.Collections;
using System;

public class Dodge : MonoBehaviour
{
    [Header("Dodge Settings")]
    public float dodgeForce;
    public float cooldown;

    [Header("Dodge Animation")]
    public AnimationClip dodgeAnimation;

    public event Action OnDodge;

    private bool canDodge = true;
    private Rigidbody2D rb;
    [SerializeField] private Collider2D col;
    private IMovement movement;
    private IImpulseMover burstMove;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movement = GetComponent<IMovement>();
        if(col == null)
        {
            col = GetComponent<Collider2D>();
        }
        burstMove = GetComponent<IImpulseMover>();
    }

    public void TryDodge()
    {
        if (!canDodge) return;
        canDodge = false;
        OnDodge?.Invoke();

        burstMove.Play(movement.GetDirection(), dodgeForce, dodgeAnimation.length);
        StartCoroutine(CooldownRoutine());
    }

    private IEnumerator CooldownRoutine()
    {
        yield return new WaitForSeconds(cooldown);
        canDodge = true;
    }

    public void EnableHitbox() => col.enabled = true;
    public void DisableHitbox() => col.enabled = false;
}
