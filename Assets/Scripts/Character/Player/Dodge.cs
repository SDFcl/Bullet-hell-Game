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

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movement = GetComponent<IMovement>();
        if(col == null)
        {
            col = GetComponent<Collider2D>();
        }
    }

    public void TryDodge()
    {
        if (!canDodge) return;
        StartCoroutine(dodge());
    }

    IEnumerator dodge()
    {
        canDodge = false;
        OnDodge?.Invoke();

        movement.DisableMovement();
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(movement.GetDirection() * dodgeForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(dodgeAnimation.length);
        movement.EnableMovement();
        
        yield return new WaitForSeconds(cooldown);
        canDodge = true;
    }

    public void EnableHitbox() => col.enabled = true;
    public void DisableHitbox() => col.enabled = false;
}
