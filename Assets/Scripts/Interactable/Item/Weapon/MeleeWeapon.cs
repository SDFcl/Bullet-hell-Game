using UnityEngine;

public class MeleeWeapon : WeaponBase
{
    private Hitbox hitbox;

    private void Awake()
    {
        hitbox = GetComponentInChildren<Hitbox>(true);
    }

    private void Start()
    {
        if (hitbox != null)
        {
            hitbox.EnableCol(false);
            hitbox.SetDamage(GetDamage());
        }
    }

    public override void SetOwner(GameObject owner)
    {
        base.SetOwner(owner);

        if (hitbox == null)
        {
            hitbox = GetComponentInChildren<Hitbox>(true);
        }

        if (hitbox != null)
            hitbox.SetOwner(owner);
    }

    protected override void OnDamageChanged()
    {
        if (hitbox != null)
            hitbox.SetDamage(GetDamage());
    }

    protected override void PerformAttack() { }

    // Animation event
    public void EnableHitbox() => hitbox.EnableCol(true);
    public void DisableHitbox() => hitbox.EnableCol(false);
}
