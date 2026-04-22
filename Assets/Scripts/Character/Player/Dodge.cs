using UnityEngine;
using System.Collections;
using System;

public class Dodge : MonoBehaviour
{
    [Header("Dodge Settings")]
    public float dodgeForce;
    public float cooldown;

    [Header("Dodge Distance")]
    public float dodgeDistanceMultiplier = 1f;

    [Header("I-Frame Settings")]
    [SerializeField] private float iFrameDelay = 0.0f;
    [SerializeField] private float iFrameDuration = 0.3f;

    [Header("Dodge Animation")]
    public AnimationClip dodgeAnimation;

    public event Action OnDodge;

    private bool canDodge = true;
    private Rigidbody2D rb;
    [SerializeField] private Collider2D col;
    private IImpulseMover burstMove;

    private Vector2 dir;

    float cooldownTimer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if(col == null)
        {
            col = GetComponent<Collider2D>();
        }
        burstMove = GetComponent<IImpulseMover>();
    }
    void Start()
    {
        cooldownTimer = cooldown;
    }
    void Update()
    {
        cooldownTimer += Time.deltaTime;
    }

    public void TryDodge()
    {
        if(cooldownTimer >= cooldown)
        {
            cooldownTimer = 0;
            if (!canDodge) return;
            StartCoroutine(DodgeRoutine());
        }
    }

    private IEnumerator DodgeRoutine()
    {
        canDodge = false;
        OnDodge?.Invoke();

        burstMove.Play(dir, dodgeForce * dodgeDistanceMultiplier, dodgeAnimation.length);

        yield return new WaitForSeconds(iFrameDelay);
        DisableHitbox();

        yield return new WaitForSeconds(iFrameDuration);
        EnableHitbox();
        canDodge = true;
    }

    public void SetDirection(Vector2 dir)
    {
        this.dir = dir;
    }

    public void EnableHitbox() => col.enabled = true;
    public void DisableHitbox() => col.enabled = false;

    public void AddDistanceMultiplier(float multiplier) => dodgeDistanceMultiplier += multiplier;
    public void RemoveDistanceMultiplier(float multiplier) => dodgeDistanceMultiplier -= multiplier;

    public void AddIFrameDuration(float amount) => iFrameDuration += amount;
    public void RemoveIFrameDuration(float amount) => iFrameDuration -= amount;
}
