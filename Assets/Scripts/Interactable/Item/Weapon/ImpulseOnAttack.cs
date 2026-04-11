using UnityEngine;

public class ImpulseOnAttack : MonoBehaviour
{
    public enum ImpulseDirectionMode
    {
        Forward,
        Backward
    }

    [Header("Impulse Settings")]
    [SerializeField] private float impulseForce = 15f;
    [SerializeField] private float impulseDuration = 0.075f;
    [SerializeField] private ImpulseDirectionMode directionMode = ImpulseDirectionMode.Forward;

    private AimPivot2D ownerAimPivot;
    private IImpulseMover impulseMover;
    private IWeapon weapon;

    private void Awake()
    {
        ownerAimPivot = GetComponentInParent<AimPivot2D>();
        impulseMover = GetComponentInParent<IImpulseMover>();
        weapon = GetComponent<IWeapon>();
    }

    private void OnEnable()
    {
        if (weapon != null)
            weapon.OnAttack += PerformEffect;
    }

    private void OnDisable()
    {
        if (weapon != null)
            weapon.OnAttack -= PerformEffect;
    }

    public void PerformEffect()
    {
        if (impulseMover == null)
            return;

        impulseMover.Play(GetImpulseDirection().normalized, impulseForce, impulseDuration);
    }

    private Vector2 GetImpulseDirection()
    {
        switch (directionMode)
        {
            case ImpulseDirectionMode.Forward:
                return ownerAimPivot != null ? ownerAimPivot.CurrentDirection : Vector2.zero;

            case ImpulseDirectionMode.Backward:
                return ownerAimPivot != null ? -ownerAimPivot.CurrentDirection : Vector2.zero;

            default:
                return Vector2.zero;
        }
    }
}
