using UnityEngine;
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
    private IImpulseMover burstMove;
    private IFrameController iframe;

    private Vector2 dir;

    float cooldownTimer;

    void Awake()
    {
        iframe = GetComponent<IFrameController>();
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
        if (cooldownTimer < cooldown)
        return;

        cooldownTimer = 0;

        burstMove.Play(dir, dodgeForce * dodgeDistanceMultiplier, dodgeAnimation.length);

        if (iframe != null)
        {
            iframe.AddDuration(iFrameDuration);
        }

        OnDodge?.Invoke();
    }

    public void SetDirection(Vector2 dir)
    {
        this.dir = dir;
    }

    public void AddDistanceMultiplier(float multiplier) => dodgeDistanceMultiplier += multiplier;
    public void RemoveDistanceMultiplier(float multiplier) => dodgeDistanceMultiplier -= multiplier;

    public void AddIFrameDuration(float amount) => iFrameDuration += amount;
    public void RemoveIFrameDuration(float amount) => iFrameDuration -= amount;
}
