using Unity.VisualScripting;
using UnityEngine;

public class LungeMeleeWP : MeleeWeapon
{
    [Header("Impulse Settings")]
    [SerializeField] private float lungeForce = 15f;
    [SerializeField] private float lungeDuration = 0.075f;

    private AimPivot2D ownerAimPivot;
    private IImpulseMover burstMove;
    public override void SetOwner(GameObject owner)
    {
        base.SetOwner(owner);
        if(owner != null)
        {
            ownerAimPivot = owner.GetComponent<AimPivot2D>();
            burstMove = owner.GetComponent<IImpulseMover>();
        }
        else
        {
            ownerAimPivot = null;
        }
    }
    protected override void PerformAttack()
    {
        burstMove.Play(ownerAimPivot.CurrentDirection, lungeForce, lungeDuration);
    }
}
